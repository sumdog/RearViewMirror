using Castle.DynamicProxy;

namespace RearViewMirror
{

    public class Proxy
    {
        private static LoggingInterceptor logger = new LoggingInterceptor();

        private static ProxyGenerator generator = new ProxyGenerator();

        public static AbstractFeedOptions wrapOptions(AbstractFeedOptions options)
        {
            return generator.CreateClassProxyWithTarget<AbstractFeedOptions>(options, logger);
        }
    }

}