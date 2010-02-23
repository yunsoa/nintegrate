using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace EAppointments.BMS.Services.STS
{
    class ServiceConstants
    {
        // Issuer name placed into issued tokens
        internal const string StsName = "BMS STS";

        // Statics for location of certs
        internal static StoreName CertStoreName = StoreName.TrustedPeople;
        internal static StoreLocation CertStoreLocation = StoreLocation.LocalMachine;

        // Statics initialized from app.config
        internal static string CertDistinguishedName;
        internal static string TargetDistinguishedName;

        #region Helper functions to load app settings from config
        /// <summary>
        /// Helper function to load Application Settings from config
        /// </summary>
        public static void LoadAppSettings()
        {
            //disabled cert validation
            //CertDistinguishedName = ConfigurationManager.AppSettings["certDistinguishedName"];
            //CheckIfLoaded(CertDistinguishedName);

            //TargetDistinguishedName = ConfigurationManager.AppSettings["targetDistinguishedName"];
            //CheckIfLoaded(TargetDistinguishedName);
        }

        /// <summary>
        /// Helper function to check if a required Application Setting has been specified in config.
        /// Throw if some Application Setting has not been specified.
        /// </summary>
        private static void CheckIfLoaded(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new ConfigurationErrorsException("Required Configuration Element(s) missing at BMSRealmSTS. Please check the STS configuration file.");
            }
        }

        #endregion

        private ServiceConstants() { }
    }
}
