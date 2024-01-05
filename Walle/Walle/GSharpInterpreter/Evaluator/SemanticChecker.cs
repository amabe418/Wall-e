﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSharpInterpreter
{
    /// <summary>
    /// Represents an evaluator that evaluates the syntax tree generated by the parser and produces the corresponding runtime values.
    /// </summary>
    public class SemanticChecker
    {
        private Scope Scope;                                // The scope of the evaluator
        private readonly int CallLimit = 500;               // The maximum amount of calls allowed
        private int Calls;                                  // The amount of calls made
        public List<GSharpError> Errors { get; private set; }     // The list of errors encountered during the evaluation

        public SemanticChecker()
        {
            Errors = new List<GSharpError>();
            Scope = new Scope();
        }
        public void Evaluate(List<Expression> AST)
        {
            // Evaluate the expressions
            foreach (Expression expression in AST)
            {
                try
                {
                    Evaluate(expression);
                }
                catch (GSharpError error)
                {
                    Errors.Add(error);
                }
                catch (Exception e)
                {
                    throw new GSharpError(ErrorType.SEMANTIC, e.Message);
                }
            }
        }
        /// <summary>
        /// Evaluates the given expression and returns the result.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        public object Evaluate(Expression expression)
        {
            // Check if the amount of calls exceeds the call limit
            if (Calls > CallLimit)
                throw new GSharpError(ErrorType.RUNTIME, "Stack Overflow.");

            // Evaluate the expression
            switch (expression)
            {
                // Statements
                case MultipleAssignment multipleAssignment:
                    EvaluateMultipleAssignment(multipleAssignment);
                    return "Constants declared succesfully.";
                case PrintStatement print:
                    EvaluatePrint(print);
                    return "Printed";
                case Assignment assign:
                    Scope.SetConstant(assign.ID, Evaluate(assign.Value));
                    return $"Constant '{assign.ID}' was declared succesfully.";
                case Function function:
                    EvaluateFunction(function);
                    return "Function declared";
                case RestoreStatement restore:
                    Scope.RestoreColor();
                    return "Color restored";
                case ColorStatement color:
                    Scope.SetColor(color.Color);
                    return "Color changed";
                case DrawStatement draw:
                    EvaluateDraw(draw);
                    return "Figure drawn";
                case ImportStatement import:
                    EvaluateImport(import);
                    return "Imported";
                case GSharpNumber number:
                    return number.Value;
                case GSharpString str:
                    return str.Value;
                case IGSharpObject gSharpObject:
                    return gSharpObject;
                case UnaryExpression unary:
                    return EvaluateUnary(unary.Operator, Evaluate(unary.Right));
                case BinaryExpression binary:
                    return EvaluateBinary(Evaluate(binary.Left), binary.Operator, Evaluate(binary.Right));
                case GroupingExpression grouping:
                    return Evaluate(grouping.Expression);
                case ConstantExpression constant:
                    return Scope.GetValue(constant.ID);

                case Conditional ifElse:
                    return EvaluateIfElse(ifElse);
                case LetExpression letIn:
                    return EvaluateLetIn(letIn);
                case Call call:
                    return EvaluateCall(call);
                case RandomDeclaration randomDeclaration:
                    return EvaluateRandomDeclaration(randomDeclaration);

                case FiniteSequenceExpression finiteSequenceExpression:
                    return EvaluateSequenceExpression(finiteSequenceExpression);
                case Sequence sequence:
                    return sequence;
                case GSharpFigure figure:
                    return figure;

                default:
                    throw new GSharpError(ErrorType.RUNTIME, "Invalid expression.");
            }
        }
        /// <summary>
        /// Imports the given file and evaluates it to add its contents to the current scope.
        /// </summary>
        private void EvaluateImport(ImportStatement import)
        {
            string path = import.Path;
            // Check if the file was already imported, return if it was cause it's already in the scope
            if (Scope.ExistsImportedFile(path))
                return;
            string extension = Path.GetExtension(path);
            // Check if the file is a .txt, .geo or .gs file
            if (!(extension == ".txt" || extension == ".geo" || extension == ".gs"))
                throw new GSharpError(ErrorType.SEMANTIC, $"File '{path}' must be a .txt, .geo or .gs file.");
            // Check if the file exists
            if (!File.Exists(path))
                throw new GSharpError(ErrorType.SEMANTIC, $"File '{path}' doesn't exist.");
            // Read the file
            string code = File.ReadAllText(path);
            // Add its contents to the current scope
            Lexer lexer = new Lexer(code);
            List<Token> tokens = lexer.ScanTokens();
            if (lexer.Errors.Count > 0)
            {
                foreach (GSharpError error in lexer.Errors)
                    Errors.Add(new GSharpError(ErrorType.SEMANTIC, $"Error importing file '{path}': {error.Message}"));
                return;
            }
            Parser parser = new Parser(tokens);
            List<Expression> AST = parser.Parse();
            if (parser.Errors.Count > 0)
            {
                foreach (GSharpError error in parser.Errors)
                    Errors.Add(new GSharpError(ErrorType.SEMANTIC, $"Error importing file '{path}': {error.Message}"));
                return;
            }
            try
            {
                Evaluate(AST);
                Scope.AddImportedFile(path);
            }
            catch (GSharpError error)
            {
                throw new GSharpError(ErrorType.SEMANTIC, $"Error importing file '{path}': {error.Message}");
            }
            catch (Exception e)
            {
                throw new GSharpError(ErrorType.SEMANTIC, $"Error importing file '{path}': {e.Message}");
            }
        }
        private void EvaluatePrint(PrintStatement print)
        {
            object result = Evaluate(print.Expression);
            string label = "";
            if (print.Label != null)
                label += " " + print.Label;
        }
        private void EvaluateDraw(DrawStatement draw)
        {
            string label = "";
            if (draw.Label != null)
                label = draw.Label;
            void DrawFigure(GSharpFigure figure, string _label = "")
            {
                return;
            }
            void Draw(Expression toDraw, string _label = "")
            {
                if (toDraw is GSharpFigure figure)
                {
                    DrawFigure(figure, _label);
                }
                else if (toDraw is FiniteSequence sequence)
                {
                    IEnumerator<Expression> enumerator = sequence.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        object _draw = Evaluate(enumerator.Current);
                        if (_draw is Measure measure)
                        {
                            throw new GSharpError(ErrorType.SEMANTIC, "Draw expression must be a sequence of figures of the same type or a figure.");
                        }
                        else
                        if (_draw is GSharpFigure || _draw is FiniteSequence)
                        {
                            Draw((Expression)draw, label);
                        }
                        else throw new GSharpError(ErrorType.SEMANTIC, "Draw expression must be a sequence of figures of the same type or a figure.");
                    }
                }
                else throw new GSharpError(ErrorType.SEMANTIC, "Draw expression must be a sequence of figures of the same type or a figure.");
            }
            object expressionToDraw = Evaluate(draw.Expression);

            if (expressionToDraw is Undefined)
                return;
            else if (expressionToDraw is GSharpFigure figure)
            {
                DrawFigure(figure, label);
            }
            else if (expressionToDraw is FiniteSequence sequence)
            {
                Draw(sequence, label);
            }
            else throw new GSharpError(ErrorType.SEMANTIC, "Draw expression must be a sequence of figures of the same type or a figure.");
        }

        private void EvaluateFunction(Function function)
        {
            // Check if the parameters are valid
            foreach (ConstantExpression parameter in function.Parameters)
            {
                if (parameter.ID == "_")
                    throw new GSharpError(ErrorType.SEMANTIC, "Function parameters can't be named '_'.");
                if (Scope.ExistsIdentifier(parameter.ID))
                    throw new GSharpError(ErrorType.SEMANTIC, $"Another identifier named '{parameter.ID}' already exists and can't be altered.");
            }
            // Reserve the parameters in the scope
            foreach (ConstantExpression parameter in function.Parameters)
            {
                Scope.Reserve(parameter.ID);
            }
            // Add the function to the declared functions
            Scope.AddFunction(function);
        }

        private void EvaluateMultipleAssignment(MultipleAssignment multipleAssignment)
        {
            List<string> variables = multipleAssignment.IDs;
            object sequence = Evaluate(multipleAssignment.Sequence);
            if (!(sequence is Sequence) && !(sequence is Undefined))
                throw new GSharpError(ErrorType.SEMANTIC, "Multiple assignment can only be done to sequences.");
            if (sequence is FiniteSequence finiteSequence)
            {
                IEnumerator<Expression> enumerator = finiteSequence.GetEnumerator();
                // Iterate through the variables except the last one
                for (int i = 0; i < variables.Count - 1; i++)
                {
                    // If there are no more elements, the variable gets an undefined value
                    if (!enumerator.MoveNext())
                        Scope.SetConstant(variables[i], new Undefined());
                    // Otherwise, the variable gets the value of the next element
                    else
                    {
                        Scope.SetConstant(variables[i], Evaluate(enumerator.Current));
                    }
                }
                // The last variable gets the rest of the sequence
                Scope.SetConstant(variables[variables.Count - 1], finiteSequence.FindSequenceTail(variables.Count - 1));
            }
            else if (sequence is InfiniteSequence infiniteSequence)
            {
                // Get the enumerator of the sequence
                IEnumerator<Expression> enumerator = infiniteSequence.GetEnumerator();
                // Iterate through the variables except the last one
                for (int i = 0; i < variables.Count - 1; i++)
                {
                    enumerator.MoveNext();
                    Scope.SetConstant(variables[i], Evaluate(enumerator.Current));
                }
                // The last variable gets the rest of the sequence
                enumerator.MoveNext();
                Scope.SetConstant(variables[variables.Count - 1], infiniteSequence.FindSequenceTail(variables.Count - 1));
            }
            else if (sequence is RangeSequence rangeSequence)
            {
                // Get the enumerator of the sequence
                IEnumerator<Expression> enumerator = rangeSequence.GetEnumerator();
                // Iterate through the variables except the last one
                for (int i = 0; i < variables.Count - 1; i++)
                {
                    // If there are no more elements, the variable gets an undefined value
                    if (enumerator.MoveNext())
                    {
                        Scope.SetConstant(variables[i], Evaluate(enumerator.Current));
                    }
                    else Scope.SetConstant(variables[i], new Undefined());
                }
                // The last variable gets the rest of the sequence
                // If there are more elements, the variable gets the rest of the sequence
                if (enumerator.MoveNext())
                {
                    if (!(enumerator.Current is GSharpNumber))
                        throw new GSharpError(ErrorType.SEMANTIC, "Range sequence must be of numbers.");
                    Scope.SetConstant(variables[variables.Count - 1], rangeSequence.FindSequenceTail(variables.Count - 1));
                }
                // If there are no more elements, the variable gets an empty sequence
                else Scope.SetConstant(variables[variables.Count - 1], new FiniteSequence(new List<Expression>()));
            }
            else foreach (string variable in variables)
                {
                    Scope.SetConstant(variable, new Undefined());
                }
        }


        /// <summary>
        /// Evaluates a binary expression by performing the corresponding operation on the left and right operands.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="Operator">The binary operator.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the binary operation.</returns>
        /*public object EvaluateBinary(object left, Token Operator, object right)
        {
            switch (Operator.Type)
            {
                case TokenType.ADDITION:
                    try
                    {
                        CheckNumbers(Operator, left, right);
                    }
                    catch(GSharpError)
                    {
                        if (left is string || right is string)
                            return left.ToString() + right.ToString();
                        throw new GSharpError(ErrorType.COMPILING, $"Operands must be Numbers or Strings in '{Operator.Lexeme}' operation.");
                    }
                    return (double)left + (double)right;


                case TokenType.SUBSTRACTION:
                    CheckNumbers(Operator, left, right);
                    return (double)left - (double)right;
                case TokenType.MULTIPLICATION:
                    CheckNumbers(Operator, left, right);
                    return (double)left * (double)right;
                case TokenType.DIVISION:
                    CheckNumbers(Operator, left, right);
                    if ((double)right != 0)
                        return (double)left / (double)right;
                    throw new GSharpError(ErrorType.COMPILING, "Division by zero is undefined.");
                case TokenType.MODULO:
                    CheckNumbers(Operator, left, right);
                    return (double)left % (double)right;
                case TokenType.POWER:
                    CheckNumbers(Operator, left, right);
                    return Math.Pow((double)left, (double)right);
                case TokenType.GREATER:
                    CheckNumbers(Operator, left, right);
                    return (double)left > (double)right;
                case TokenType.GREATER_EQUAL:
                    CheckNumbers(Operator, left, right);
                    return (double)left >= (double)right;
                case TokenType.LESS:
                    CheckNumbers(Operator, left, right);
                    return (double)left < (double)right;
                case TokenType.LESS_EQUAL:
                    CheckNumbers(Operator, left, right);
                    return (double)left <= (double)right;

                case TokenType.EQUAL:
                    return IsEqual(left, right);
                case TokenType.NOT_EQUAL:
                    return !IsEqual(left, right);

                case TokenType.AND:
                    CheckBooleans(Operator, left, right);
                    return (bool)left && (bool)right;
                case TokenType.OR:
                    CheckBooleans(Operator, left, right);
                    return (bool)left || (bool)right;

                default:
                    return null;
            }
        }*/
        public object EvaluateBinary(object left, Token Operator, object right)
        {
            if (left is Undefined || right is Undefined)
                return left;
            if (left is Sequence leftSequence && right is Sequence rightSequence)
            {
                return leftSequence + rightSequence;
            }
            if (left is string || right is string)
                return left.ToString() + right.ToString();

            if (left is double leftNumber && right is double rightNumber)
            {
                switch (Operator.Type)
                {
                    case TokenType.ADDITION:
                        return leftNumber + rightNumber;
                    case TokenType.SUBSTRACTION:
                        return leftNumber - rightNumber;
                    case TokenType.MULTIPLICATION:
                        return leftNumber * rightNumber;
                    case TokenType.POWER:
                        return Math.Pow(leftNumber, rightNumber);
                    case TokenType.GREATER:
                        if (leftNumber > rightNumber) return (double)1;
                        else return (double)0;
                    case TokenType.GREATER_EQUAL:
                        if (leftNumber >= rightNumber) return (double)1;
                        else return (double)0;
                    case TokenType.LESS:
                        if (leftNumber < rightNumber) return (double)1;
                        else return (double)0;
                    case TokenType.LESS_EQUAL:
                        if (leftNumber <= rightNumber) return (double)1;
                        else return (double)0;
                    case TokenType.EQUAL:
                        if (leftNumber == rightNumber) return (double)1;
                        else return (double)0;
                    case TokenType.NOT_EQUAL:
                        if (leftNumber != rightNumber) return (double)1;
                        else return (double)0;
                    case TokenType.DIVISION:
                        if (rightNumber == 0) throw new GSharpError(ErrorType.SEMANTIC, "Division by zero is undefined.");
                        return (double)leftNumber / (double)rightNumber;
                    case TokenType.MODULO:
                        if (rightNumber == 0) throw new GSharpError(ErrorType.SEMANTIC, "Modulo by zero is undefined.");
                        return (double)leftNumber % (double)rightNumber;
                    case TokenType.AND:
                        if (leftNumber != 0 && rightNumber != 0) return (double)1;
                        else return (double)0;
                    case TokenType.OR:
                        if (leftNumber == 1 || rightNumber == 1) return (double)1;
                        else return (double)0;
                    default:
                        throw new GSharpError(ErrorType.SEMANTIC, $"Cannot perform operation '{Operator.Lexeme}' on {left.ToString()} and {right.ToString()}.");
                }
            }
            else if (left is Measure leftMeasure && right is Measure rightMeasure)
            {
                switch (Operator.Type)
                {
                    case TokenType.ADDITION:
                        return new Measure(leftMeasure.Value + rightMeasure.Value);
                    case TokenType.SUBSTRACTION:
                        return new Measure(Math.Abs(leftMeasure.Value - rightMeasure.Value));
                    case TokenType.DIVISION:
                        if (rightMeasure.Value == 0) throw new GSharpError(ErrorType.SEMANTIC, "Right measure is zero and division by zero is undefined.");
                        return (double)(leftMeasure.Value / rightMeasure.Value);
                    case TokenType.GREATER:
                        return (leftMeasure.Value > rightMeasure.Value) ? (double)1 : (double)0;
                    case TokenType.GREATER_EQUAL:
                        return (leftMeasure.Value >= rightMeasure.Value) ? (double)1 : (double)0;
                    case TokenType.LESS:
                        return (leftMeasure.Value < rightMeasure.Value) ? (double)1 : (double)0;
                    case TokenType.LESS_EQUAL:
                        return (leftMeasure.Value <= rightMeasure.Value) ? (double)1 : (double)0;
                    case TokenType.EQUAL:
                        return (leftMeasure.Value == rightMeasure.Value) ? (double)1 : (double)0;
                    case TokenType.NOT_EQUAL:
                        return (leftMeasure.Value != rightMeasure.Value) ? (double)1 : (double)0;
                    default:
                        throw new GSharpError(ErrorType.SEMANTIC, $"Cannot perform operation '{Operator.Lexeme}' on {left.ToString()} and {right.ToString()}.");
                }
            }
            else if (left is Measure measureLeft && right is double numberRight)
            {
                if (Operator.Type == TokenType.MULTIPLICATION) return new Measure(measureLeft.Value * Math.Abs((int)numberRight));
                else throw new GSharpError(ErrorType.SEMANTIC, $"Cannot perform operation '{Operator.Lexeme}' on {left.ToString()} and {right.ToString()}.");
            }
            else if (left is double numberLeft && right is Measure measureRight)
            {
                if (Operator.Type == TokenType.MULTIPLICATION) return new Measure(measureRight.Value * Math.Abs((int)numberLeft));
                else throw new GSharpError(ErrorType.SEMANTIC, $"Cannot perform operation '{Operator.Lexeme}' on {left.ToString()} and {right.ToString()}.");
            }
            throw new GSharpError(ErrorType.SEMANTIC, $"Cannot perform operation '{Operator.Lexeme}' on {left.ToString()} and {right.ToString()}.");
        }

        /// <summary>
        /// Evaluates a unary expression by performing the corresponding operation on the right operand.
        /// </summary>
        /// <param name="Operator">The unary operator.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>The result of the unary operation.</returns>
        public object EvaluateUnary(Token Operator, object right)
        {
            switch (Operator.Type)
            {
                case TokenType.NOT:
                    if (right is bool)
                        return !(bool)right;
                    if (right is double)
                        return (double)right == 0 ? (double)1 : (double)0;
                    if (right is Sequence sequence)
                    {
                        if (sequence.TotalCount != 0 || double.IsPositiveInfinity(sequence.TotalCount))
                            return (double)0;
                        else return (double)1;
                    }
                    if (right is Undefined)
                        return (double)1;
                    throw new GSharpError(ErrorType.RUNTIME, $"Cannot perform operation '{Operator.Lexeme}' on {right.ToString()}.");
                case TokenType.SUBSTRACTION:
                    if (right is double)
                        return -(double)right;
                    else throw new GSharpError(ErrorType.RUNTIME, $"Cannot perform operation '{Operator.Lexeme}' on {right.ToString()}.");
                default:
                    throw new GSharpError(ErrorType.RUNTIME, $"Cannot perform operation '{Operator.Lexeme}' on {right.ToString()}.");
            }
        }
        /// <summary>
        /// Evaluates a constant expression by retrieving its value from the current scope.
        /// </summary>
        /// <param name="name">The name of the constant.</param>
        /// <returns>The value of the constant.</returns>
        public object EvaluateConstant(string name)
        {
            return Scope.GetValue(name);
        }
        /// <summary>
        /// Evaluates a let-in expression by creating a new scope, declaring the variables and evaluating the body expression.
        /// </summary>
        /// <param name="letIn">The let-in expression to evaluate.</param>
        /// <returns>The result of the body expression.</returns>
        public object EvaluateLetIn(LetExpression letIn)
        {
            //PushScope();
            Scope.EnterScope();
            foreach (Expression instruction in letIn.Instructions)
            {
                Evaluate(instruction);
            }
            object result = Evaluate(letIn.Body);
            //PopScope();
            Scope.ExitScope();
            return result;
        }
        /// <summary>
        /// Evaluates an if-else statement by evaluating the condition and either the then branch or the else branch based on the result.
        /// </summary>
        /// <param name="ifElse">The if-else statement to be evaluated.</param>
        /// <returns>The evaluated value of the executed branch.</returns>
        public object EvaluateIfElse(Conditional ifElse)
        {
            object condition = Evaluate(ifElse.Condition);
            /*if (!IsBoolean(condition))
                throw new GSharpError(ErrorType.SEMANTIC, "Condition in 'If-Else' expression must be a boolean expression.");
            return (bool)condition ? Evaluate(ifElse.ThenBranch) : Evaluate(ifElse.ElseBranch);*/
            if (condition is GSharpNumber)
            {
                if (((GSharpNumber)condition).Value == 0)
                    return Evaluate(ifElse.ElseBranch);
                else return Evaluate(ifElse.ThenBranch);
            }
            if (condition is double)
            {
                if ((double)condition == 0)
                    return Evaluate(ifElse.ElseBranch);
                else return Evaluate(ifElse.ThenBranch);
            }
            else if (condition is Sequence sequence)
            {
                if (double.IsPositiveInfinity(sequence.TotalCount) || sequence.TotalCount != 0)
                    return Evaluate(ifElse.ThenBranch);
                else
                    return Evaluate(ifElse.ElseBranch);
            }
            else if (condition is Undefined)
                return Evaluate(ifElse.ElseBranch);
            else return Evaluate(ifElse.ThenBranch);
        }
        /// <summary>
        /// Evaluates a function call expression by evaluating the arguments and executing the function.
        /// </summary>
        /// <param name="call">The function call to evaluate.</param>
        /// <returns>The result of the evaluated function.</returns>
        public object EvaluateCall(Call call)
        {
            Calls++;
            // Evaluate the arguments
            List<object> arguments = new List<object>();
            foreach (Expression arg in call.Arguments)
                arguments.Add(Evaluate(arg));

            // Check if the call is to a predefined function
            if (StandardLibrary.PredefinedFunctions.ContainsKey(call.Identifier))
                return StandardLibrary.PredefinedFunctions[call.Identifier](arguments);

            // Get the function declaration
            Function function = Scope.GetFunction(call.Identifier);
            // Check the amount of arguments of the function vs the arguments passed
            if (arguments.Count != function.Parameters.Count)
                throw new GSharpError(ErrorType.SEMANTIC, $"Function '{call.Identifier}' receives '{arguments.Count}' argument(s) instead of the correct amount '{function.Parameters.Count}'");

            Scope.EnterScope();
            // Add the evaluated arguments to the scope
            for (int i = 0; i < function.Parameters.Count; i++)
            {
                string parameterName = function.Parameters[i].ID;
                object argumentValue = arguments[i];
                Scope.SetArgument(parameterName, argumentValue);
            }
            // Evaluate the body of the function
            object result = Evaluate(function.Body);
            Scope.ExitScope();
            Calls--;
            return result;
        }
        /// <summary>
        /// Evaluates a random declaration by generating a random value of the given type.
        /// </summary>
        private object EvaluateRandomDeclaration(RandomDeclaration randomDeclaration)
        {
            object result = 0;
            if (randomDeclaration.IsSequence)
            {
                switch (randomDeclaration.Type)
                {
                    case GSharpType.POINT:
                        result = StandardLibrary.RandomPointSequence();
                        break;
                    case GSharpType.LINE:
                        result = StandardLibrary.RandomLineSequence();
                        break;
                    case GSharpType.SEGMENT:
                        result = StandardLibrary.RandomSegmentSequence();
                        break;
                    case GSharpType.RAY:
                        result = StandardLibrary.RandomRaySequence();
                        break;
                    case GSharpType.CIRCLE:
                        result = StandardLibrary.RandomCircleSequence();
                        break;
                    case GSharpType.ARC:
                        result = StandardLibrary.RandomArcSequence();
                        break;
                }
            }
            else
            {
                switch (randomDeclaration.Type)
                {
                    case GSharpType.POINT:
                        result = StandardLibrary.RandomPoint();
                        break;
                    case GSharpType.LINE:
                        result = StandardLibrary.RandomLine();
                        break;
                    case GSharpType.SEGMENT:
                        result = StandardLibrary.RandomSegment();
                        break;
                    case GSharpType.RAY:
                        result = StandardLibrary.RandomRay();
                        break;
                    case GSharpType.CIRCLE:
                        result = StandardLibrary.RandomCircle();
                        break;
                    case GSharpType.ARC:
                        result = StandardLibrary.RandomArc();
                        break;
                }
            }
            return result;
        }
        public object EvaluateSequenceExpression(FiniteSequenceExpression finiteSequenceExpression)
        {
            List<Expression> elements = finiteSequenceExpression.Elements;
            if (elements.Count == 0)
                return new FiniteSequence(new List<Expression>(), GSharpType.EMPTY);
            List<object> evaluatedElements = new List<object>();
            List<GSharpType> elementTypes = new List<GSharpType>();
            foreach (Expression element in elements)
            {
                if (element is IGSharpObject)
                    evaluatedElements.Add(element);
                else if (element is ConstantExpression)
                    evaluatedElements.Add(Evaluate(element));
                else throw new GSharpError(ErrorType.SEMANTIC, "Elements of a sequence must be written as constants or literals.");
            }
            foreach (object element in evaluatedElements)
            {
                if (element is double)
                    elementTypes.Add(GSharpType.NUMBER);
                else if (element is string)
                    elementTypes.Add(GSharpType.STRING);
                else if (element is IGSharpObject)
                    elementTypes.Add(((IGSharpObject)element).Type);
                else throw new GSharpError(ErrorType.SEMANTIC, "Elements of a sequence must be of type Number, String, Point, Line, Segment, Ray, Circle or Arc.");
            }
            GSharpType elementType = elementTypes[0];
            foreach (GSharpType type in elementTypes)
            {
                if (type != elementType)
                    throw new GSharpError(ErrorType.SEMANTIC, "Sequence elements must be of the same type.");
            }
            return new FiniteSequence(elements, elementType);
        }

        #region Helper Methods

        /// <summary>
        /// Checks if the operand is a boolean value. Throws a semantic error if it is not.
        /// </summary>
        /// <param name="Operator">The operator token.</param>
        /// <param name="right">The operand to check.</param>
        public void CheckBoolean(Token Operator, object right)
        {
            if (IsBoolean(right)) return;
            throw new GSharpError(ErrorType.SEMANTIC, $"Operand must be Boolean in '{Operator.Lexeme}' operation.");
        }
        /// <summary>
        /// Checks if the operands are boolean values. Throws a semantic error if they are not.
        /// </summary>
        /// <param name="Operator">The operator token.</param>
        /// <param name="left">The left operand to check.</param>
        /// <param name="right">The right operand to check.</param>
        public void CheckBooleans(Token Operator, object left, object right)
        {
            if (IsBoolean(left, right)) return;
            throw new GSharpError(ErrorType.SEMANTIC, $"Operands must be Boolean in '{Operator.Lexeme}' operation.");
        }
        /// <summary>
        /// Checks if the operand is a number. Throws a semantic error if it is not.
        /// </summary>
        /// <param name="Operator">The operator token.</param>
        /// <param name="right">The operand to check.</param>
        public void CheckNumber(Token Operator, object right)
        {
            if (IsNumber(right)) return;
            throw new GSharpError(ErrorType.SEMANTIC, $"Operand must be Number in '{Operator.Lexeme}' operation.");
        }
        /// <summary>
        /// Checks if the operands are numbers. Throws a semantic error if they are not.
        /// </summary>
        /// <param name="Operator">The operator token.</param>
        /// <param name="left">The left operand to check.</param>
        /// <param name="right">The right operand to check.</param>
        public void CheckNumbers(Token Operator, object left, object right)
        {
            if (IsNumber(left, right)) return;
            throw new GSharpError(ErrorType.SEMANTIC, $"Operands must be Numbers in '{Operator.Lexeme}' operation.");
        }
        /// <summary>
        /// Checks if the given operands are number values.
        /// </summary>
        /// <param name="operands">The operands to check.</param>
        /// <returns><c>true</c> if all operands are numbers; otherwise, <c>false</c>.</returns>
        public bool IsNumber(params object[] operands)
        {
            foreach (object operand in operands)
            {
                if (!(operand is double))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Checks if the given operands are boolean values.
        /// </summary>
        /// <param name="operands">The operands to check.</param>
        /// <returns><c>true</c> if all operands are booleans; otherwise, <c>false</c>.</returns>
        public bool IsBoolean(params object[] operands)
        {
            foreach (object operand in operands)
            {
                if (!(operand is bool))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Checks if the left and right operands are equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns><c>true</c> if the operands are equal; otherwise, <c>false</c>.</returns>
        public bool IsEqual(object left, object right)
        {
            if (left == null && right == null)
                return true;
            return left == null ? false : left.Equals(right);
        }

        #endregion
    }
}
