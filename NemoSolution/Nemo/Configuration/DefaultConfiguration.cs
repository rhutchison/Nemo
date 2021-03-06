﻿using Nemo.Caching;
using Nemo.Caching.Providers;
using Nemo.Extensions;
using Nemo.Serialization;
using Nemo.UnitOfWork;
using Nemo.Utilities;
using System;

namespace Nemo.Configuration
{
    public sealed class DefaultConfiguration : IConfiguration
    {
        private string _defaultConnectionName = Config.AppSettings("DefaultConnectionName", "DbConnection");
        private string _operationPrefix = Config.AppSettings("OperationPrefix", string.Empty);
        private bool _distributedLockVerification = Config.AppSettings("EnableDistributedLockVerification", false);
        private int _defaultCacheLifeTime = Config.AppSettings("DefaultCacheLifeTime", 900);
        private bool _cacheCollisionDetection = Config.AppSettings("EnableCacheCollisionDetection", false);
        private bool _logging = Config.AppSettings("EnableLogging", false);
        private string _secretKey = Config.AppSettings("SecretKey", Bytes.ToHex(Bytes.Random(10)));
        private int _staleCacheTimeout = Config.AppSettings("StaleCacheTimeout", 2);
        private int _distributedLockTimeout = Config.AppSettings("DistributedLockTimeout", 2);
        private int _distributedLockRetryCount = Config.AppSettings("DistributedLockRetryCount", 4);
        private double _distributedLockWaitTime = Config.AppSettings("DistributedLockWaitTime", 0.7);
        
        private ContextLevelCacheType _defaultContextLevelCache = ParseContextLevelCacheConfig();
        private OperationNamingConvention _operationNamingConvention = ParseOperationNamingConventionConfig();
        private FetchMode _defaultFetchMode = ParseFetchModeConfig();
        private MaterializationMode _defaultMaterializationMode = ParseMaterializationModeConfig();
        private ChangeTrackingMode _defaultChangeTrackingMode = ParseChangeTrackingModeConfig();
        private HashAlgorithmName _defaultHashAlgorithm = ParseHashAlgorithmNameConfig();
        private CacheContentionMitigationType _cacheContentionMitigation = ParseCacheContentionMitigationTypeConfig();
        private BinarySerializationMode _defaultBinarySerializationMode = ParseBinarySerializationModeConfig();

       
        private bool _generateDeleteSql = false;
        private bool _generateInsertSql = false;
        private bool _generateUpdateSql = false;
        private Type _cacheProvider = typeof(MemoryCacheProvider);
        
        private DefaultConfiguration() { }

        public bool DistributedLockVerification
        {
            get
            {
                return _distributedLockVerification;
            }
        }

        public ContextLevelCacheType DefaultContextLevelCache
        {
            get
            {
                return _defaultContextLevelCache;
            }
        }

        public int DefaultCacheLifeTime
        {
            get
            {
                return _defaultCacheLifeTime;
            }
        }

        public bool CacheCollisionDetection
        {
            get
            {
                return _cacheCollisionDetection;
            }
        }

        public bool Logging
        {
            get
            {
                return _logging;
            }
        }

        public FetchMode DefaultFetchMode
        {
            get
            {
                return _defaultFetchMode;
            }
        }

        public MaterializationMode DefaultMaterializationMode
        {
            get
            {
                return _defaultMaterializationMode;
            }
        }

        public string DefaultConnectionName
        {
            get
            {
                return _defaultConnectionName;
            }
        }

        public string OperationPrefix
        {
            get
            {
                return _operationPrefix;
            }
        }

        public ChangeTrackingMode DefaultChangeTrackingMode
        {
            get
            {
                return _defaultChangeTrackingMode;
            }
        }

        public OperationNamingConvention OperationNamingConvention
        {
            get
            {
                return _operationNamingConvention;
            }
        }

        public HashAlgorithmName DefaultHashAlgorithm
        {
            get
            {
                return _defaultHashAlgorithm;
            }
        }

        public string SecretKey
        {
            get
            {
                return _secretKey;
            }
        }

        public CacheContentionMitigationType CacheContentionMitigation
        {
            get
            {
                return _cacheContentionMitigation;
            }
        }

        public int StaleCacheTimeout
        {
            get
            {
                return _staleCacheTimeout;
            }
        }

        public int DistributedLockTimeout
        {
            get
            {
                return _distributedLockTimeout;
            }
        }

        public int DistributedLockRetryCount
        {
            get
            {
                return _distributedLockRetryCount;
            }
        }

        public double DistributedLockWaitTime
        {
            get
            {
                return _distributedLockWaitTime;
            }
        }

        public BinarySerializationMode DefaultBinarySerializationMode
        {
            get
            {
                return _defaultBinarySerializationMode;
            }
        }

        public bool GenerateDeleteSql
        {
            get
            {
                return _generateDeleteSql;
            }
        }

        public bool GenerateInsertSql
        {
            get
            {
                return _generateInsertSql;
            }
        }

        public bool GenerateUpdateSql
        {
            get
            {
                return _generateUpdateSql;
            }
        }

        public Type DefaultCacheProvider
        {
            get
            {
                return _cacheProvider;
            }
        }

        public static IConfiguration New()
        {
            return new DefaultConfiguration();
        }

        public IConfiguration SetDistributedLockVerification(bool value)
        {
            _distributedLockVerification = value;
            return this;
        }

        public IConfiguration SetDefaultContextLevelCache(ContextLevelCacheType value)
        {
            _defaultContextLevelCache = value;
            return this;
        }

        public IConfiguration SetDefaultCacheLifeTime(int value)
        {
            _defaultCacheLifeTime = value;
            return this;
        }

        public IConfiguration SetCacheCollisionDetection(bool value)
        {
            _cacheCollisionDetection = value;
            return this;
        }

        public IConfiguration SetLogging(bool value)
        {
            _logging = value;
            return this;
        }

        public IConfiguration SetDefaultFetchMode(FetchMode value)
        {
            if (value != FetchMode.Default)
            {
                _defaultFetchMode = value;
            }
            return this;
        }

        public IConfiguration SetDefaultMaterializationMode(MaterializationMode value)
        {
            if (value != MaterializationMode.Default)
            {
                _defaultMaterializationMode = value;
            }
            return this;
        }

        public IConfiguration SetOperationPrefix(string value)
        {
            _operationPrefix = value;
            return this;
        }

        public IConfiguration SetDefaultConnectionName(string value)
        {
            _defaultConnectionName = value;
            return this;
        }

        public IConfiguration SetDefaultChangeTrackingMode(ChangeTrackingMode value)
        {
            if (value != ChangeTrackingMode.Default)
            {
                _defaultChangeTrackingMode = value;
            }
            return this;
        }

        public IConfiguration SetOperationNamingConvention(OperationNamingConvention value)
        {
            if (value != OperationNamingConvention.Default)
            {
                _operationNamingConvention = value;
            }
            return this;
        }

        public IConfiguration SetDefaultHashAlgorithm(HashAlgorithmName value)
        {
            if (value != HashAlgorithmName.Default)
            {
                _defaultHashAlgorithm = value;
            }
            return this;
        }

        public IConfiguration SetSecretKey(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _secretKey = value;
            }
            return this;
        }

        public IConfiguration SetCacheContentionMitigation(CacheContentionMitigationType value)
        {
            _cacheContentionMitigation = value;
            return this;
        }

        public IConfiguration SetStaleCacheTimeout(int value)
        {
            if (value < 1)
            {
                value = 1;
            }
            _staleCacheTimeout = value;
            return this;
        }

        public IConfiguration SetDistributedLockTimeout(int value)
        {
            if (value < 1)
            {
                value = 1;
            }
            _distributedLockTimeout = value;
            return this;
        }

        public IConfiguration SetDistributedLockRetryCount(int value)
        {
            if (value > -1)
            {
                _distributedLockRetryCount = value;
            }
            return this;
        }

        public IConfiguration SetDistributedLockWaitTime(double value)
        {
            if (value > 0)
            {
                _distributedLockWaitTime = value;
            }
            return this;
        }

        public IConfiguration SetDefaultBinarySerializationMode(BinarySerializationMode value)
        {
            _defaultBinarySerializationMode = value;
            return this;
        }

        public IConfiguration SetGenerateDeleteSql(bool value)
        {
            _generateDeleteSql = value;
            return this;
        }

        public IConfiguration SetGenerateInsertSql(bool value)
        {
            _generateInsertSql = value;
            return this;
        }

        public IConfiguration SetGenerateUpdateSql(bool value)
        {
            _generateUpdateSql = value;
            return this;
        }

        public IConfiguration SetDefaultCacheProvider(Type value)
        {
            if (value == null || typeof(CacheProvider).IsAssignableFrom(value))
            {
                _cacheProvider = value;
            }
            return this;
        }
        
        private static ContextLevelCacheType ParseContextLevelCacheConfig()
        {
            ContextLevelCacheType result;
            var value = Config.AppSettings("DefaultContextLevelCache");
            if (value == null || !Enum.TryParse<ContextLevelCacheType>(value, true, out result))
            {
                result = ContextLevelCacheType.LazyList;
            }
            return result;
        }

        private static OperationNamingConvention ParseOperationNamingConventionConfig()
        {
            OperationNamingConvention result;
            var value = Config.AppSettings("OperationNamingConvention");
            if (value == null || !Enum.TryParse<OperationNamingConvention>(value, true, out result))
            {
                result = OperationNamingConvention.PrefixTypeName_Operation;
            }
            return result;
        }

        private static FetchMode ParseFetchModeConfig()
        {
            FetchMode result;
            var value = Config.AppSettings("DefaultFetchMode");
            if (value == null || !Enum.TryParse<FetchMode>(value, true, out result))
            {
                result = FetchMode.Eager;
            }
            return result;
        }

        private static MaterializationMode ParseMaterializationModeConfig()
        {
            MaterializationMode result;
            var value = Config.AppSettings("DefaultMaterializationMode");
            if (value == null || !Enum.TryParse<MaterializationMode>(value, true, out result))
            {
                result = MaterializationMode.Partial;
            }
            return result;
        }

        private static ChangeTrackingMode ParseChangeTrackingModeConfig()
        {
            ChangeTrackingMode result;
            var value = Config.AppSettings("DefaultChangeTrackingMode");
            if (value == null || !Enum.TryParse<ChangeTrackingMode>(value, true, out result))
            {
                result = ChangeTrackingMode.Automatic;
            }
            return result;
        }

        private static HashAlgorithmName ParseHashAlgorithmNameConfig()
        {
            HashAlgorithmName result;
            var value = Config.AppSettings("DefaultHashAlgorithmName");
            if (value == null || !Enum.TryParse<HashAlgorithmName>(value, true, out result))
            {
                result = HashAlgorithmName.JenkinsHash;
            }
            return result;
        }


        private static CacheContentionMitigationType ParseCacheContentionMitigationTypeConfig()
        {
            CacheContentionMitigationType result;
            var value = Config.AppSettings("CacheContentionMitigationType");
            if (value == null || !Enum.TryParse<CacheContentionMitigationType>(value, true, out result))
            {
                result = CacheContentionMitigationType.None;
            }
            return result;
        }

        private static BinarySerializationMode ParseBinarySerializationModeConfig()
        {
            BinarySerializationMode result;
            var value = Config.AppSettings("DefaultBinarySerializationMode");
            if (value == null || !Enum.TryParse<BinarySerializationMode>(value, true, out result))
            {
                result = BinarySerializationMode.IncludePropertyNames;
            }
            return result;
        }
    }
}
