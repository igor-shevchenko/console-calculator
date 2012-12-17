using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ConsoleCalculator.Detectors;
using ConsoleCalculator.Operations;
using ConsoleCalculator.Tokens;
using ConsoleCalculator.Tree;

namespace ConsoleCalculator
{
    class ContainerFactory
    {
        public IWindsorContainer GetContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ISplitter>().ImplementedBy<Splitter>());
            container.Register(Component.For<ITokenizer>().ImplementedBy<Tokenizer>());
            container.Register(Component.For<IExpressionTreeBuilder>().ImplementedBy<ExpressionTreeBuilder>());
            container.Register(Component.For<IOperatorFactory>().ImplementedBy<OperatorFactory>());
            container.Register(Component.For<ICalculator>().ImplementedBy<Calculator>());
            container.Register(Component.For<IOperatorListFactory>().ImplementedBy<OperatorListFactory>());
            container.Register(Component.For<IOperationSignDetector>().ImplementedBy<OperationSignDetector>());
            container.Register(Component.For<IBracketSignDetector>().ImplementedBy<BracketSignDetector>());
            container.Register(Component.For<ISeparatorDetector>().ImplementedBy<SeparatorDetector>());
            container.Register(Component.For<ITokenTypeIdentifier>().ImplementedBy<TokenTypeIdentifier>());
            container.Register(Component.For<IBracketValidator>().ImplementedBy<BracketValidator>());
            container.Register(Component.For<ITokenBuilder>().ImplementedBy<TokenBuilder>());
            
            return container;
        }
    }
}