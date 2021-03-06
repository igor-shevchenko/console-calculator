using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ConsoleCalculator.Operations;
using ConsoleCalculator.Tokenization;
using ConsoleCalculator.Tokens;
using ConsoleCalculator.Tree;

namespace ConsoleCalculator
{
    class ContainerFactory
    {
        public IWindsorContainer GetContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IExpressionTreeBuilder>().ImplementedBy<ExpressionTreeBuilder>());
            container.Register(Component.For<ICalculator>().ImplementedBy<Calculator>());
            container.Register(Component.For<IOperatorListFactory>().ImplementedBy<OperatorListFactory>());
            container.Register(Component.For<IOperatorLoader>().ImplementedBy<OperatorLoader>());
            container.Register(Component.For<ILexer>().ImplementedBy<Lexer>());
            container.Register(Component.For<ILexerRuleListFactory>().ImplementedBy<LexerRuleListFactory>());
            return container;
        }
    }
}