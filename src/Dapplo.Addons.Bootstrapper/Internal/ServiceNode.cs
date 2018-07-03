using System.Collections.Generic;

namespace Dapplo.Addons.Bootstrapper.Internal
{
    /// <summary>
    /// This contains the information needed for the startup and shutdown of services
    /// </summary>
    public class ServiceNode
    {
        private bool _isShutdownStarted;

        /// <summary>
        /// The attributed details
        /// </summary>
        public ServiceAttribute Details { get; set; }

        /// <summary>
        /// Used to define if the Shutdown was already started
        /// </summary>
        public bool StartShutdown()
        {
            lock (this)
            {
                if (_isShutdownStarted)
                {
                    return false;
                }

                _isShutdownStarted = true;
                return true;
            }
        }

        /// <summary>
        /// Task of the service
        /// </summary>
        public IService Service { get; set; }

        /// <summary>
        /// Test if this service depends on other services
        /// </summary>
        public bool IsDependendOn => DependensOn.Count > 0;

        /// <summary>
        /// The service which should be started before this
        /// </summary>
        public IList<ServiceNode> DependensOn { get; } = new List<ServiceNode>();

        /// <summary>
        /// Test if this service has dependencies
        /// </summary>
        public bool HasDependencies => Dependencies.Count > 0;

        /// <summary>
        /// The services awaiting for this
        /// </summary>
        public IList<ServiceNode> Dependencies { get; } = new List<ServiceNode>();
    }
}
