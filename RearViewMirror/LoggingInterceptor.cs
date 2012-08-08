using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using MJPEGServer;

namespace RearViewMirror
{

    [Serializable]
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {

            Log.trace(String.Format("start {0}.{1}({2})",
                invocation.TargetType.Name,
                invocation.Method.Name,
                String.Join(",", invocation.Arguments)
                )
            );
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                Log.error(String.Format("exception {0}.{1} :: {2}",
                    invocation.TargetType.Name,
                    invocation.Method.Name,
                    e.Message
                    )
                );
                throw e;
            }
            finally
            {
                Log.trace(String.Format("exit ({0}) {1}.{2}",
                    invocation.ReturnValue,
                    invocation.TargetType.Name,
                    invocation.Method.Name
                    )
                );
            }
        }
    }
}
