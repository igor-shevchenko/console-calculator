using Castle.MicroKernel.Registration;
using Castle.Windsor;
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
            return container;
        }
    }
}