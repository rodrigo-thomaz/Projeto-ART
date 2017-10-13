using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System;
using System.Configuration;

namespace ART.Infra.CrossCutting.Setting
{
    public class SettingManager : ISettingManager
    {
        private readonly SqlConnection _connection;

        public SettingManager()
        {
            var connectionString = GetConnectionString();
            _connection = new SqlConnection(connectionString);
        }

        public async Task<bool> Exist(string key)
        {
            KeyValidator(key);

            await _connection.OpenAsync();

            SqlCommand command = null;
            bool data;

            try
            {
                command = CreateCommand();
                command.CommandText = ExistsCommandText(key);
                data = Convert.ToBoolean(await command.ExecuteScalarAsync());
            }
            finally
            {
                command.Dispose();
                _connection.Close();
            }

            return data;
        }        

        public async Task Insert<T>(string key, T value)
        {
            KeyValidator(key);

            await _connection.OpenAsync();

            SqlCommand command = null;            

            int result = 0;

            try
            {
                command = CreateCommand();
                command.CommandText = InsertCommandText(key, value);
                result = await command.ExecuteNonQueryAsync();
            }
            finally
            {
                command.Dispose();
                _connection.Close();
            }            

            if (result != 1)
            {
                throw new Exception("Ops, ocorreu um erro inesperado inserindo um setting");
            }
        }

        public async Task Delete(string key)
        {
            KeyValidator(key);

            await _connection.OpenAsync();

            SqlCommand command = null;            

            int result = 0;

            try
            {
                command = CreateCommand();
                command.CommandText = DeleteCommandText(key);
                result = await command.ExecuteNonQueryAsync();
            }
            finally
            {
                command.Dispose();
                _connection.Close();
            }

            if (result == 0)
            {
                throw new Exception("Key not found");
            }
        }

        public async Task<T> GetValue<T>(string key)
        {
            KeyValidator(key);

            await _connection.OpenAsync();

            SqlCommand command = null;
            SqlDataReader reader = null;
            object data = null;

            try
            {
                command = CreateCommand();
                command.CommandText = GetCommandText(key);
                reader = await command.ExecuteReaderAsync();

                if (!await reader.ReadAsync())
                {
                    throw new Exception("Key not found");
                }

                data = reader["Value"];
            }
            finally
            {
                reader.Close();
                command.Dispose();
                _connection.Close();
            }            
            
            return (T)Convert.ChangeType(data, typeof(T));
        }

        public async Task SetValue<T>(string key, T value)
        {
            KeyValidator(key);

            await _connection.OpenAsync();

            SqlCommand command = null;
            int data;

            try
            {
                command = CreateCommand();
                command.CommandText = SetCommandText(key, value);
                data = await command.ExecuteNonQueryAsync();
            }
            finally
            {
                command.Dispose();
                _connection.Close();
            }

            if (data == 0)
            {
                throw new Exception("Key not found");
            }
        }

        private void KeyValidator(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
        }

        private SqlCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            return command;
        }

        private string ExistsCommandText(string key)
        {
            return string.Format("SELECT COUNT([Key]) FROM Setting WHERE [Key] = '{0}'", key);
        }

        private string InsertCommandText<T>(string key, T value)
        {
            return string.Format("INSERT INTO Setting ([Key], Value) Values ('{0}', '{1}')", key, value);
        }

        private string DeleteCommandText(string key)
        {
            return string.Format("DELETE FROM Setting WHERE [Key] = '{0}'", key);
        }

        private string GetCommandText(string key)
        {
            return string.Format("SELECT [Key], Value FROM Setting WHERE [Key] = '{0}'", key);
        }

        private string SetCommandText<T>(string key, T value)
        {
            return string.Format("UPDATE Setting SET Value = '{0}' WHERE [Key] = '{1}'", value, key);
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["ART.Setting.ConnectionString"];
        }        

        public void Dispose()
        {
            _connection.Dispose();
        }        
    }
}
