﻿using API_PROGRAMACION_DE_SOFTWARE.Controllers;
using API_PROGRAMACION_DE_SOFTWARE.Entities;
using API_PROGRAMACION_DE_SOFTWARE.Interfaces;
using API_PROGRAMACION_DE_SOFTWARE.Queries;
using API_PROGRAMACION_DE_SOFTWARE.Utilities;
using API_PROGRAMACION_DE_SOFTWARE.Enumerations;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using API_PROGRAMACION_DE_SOFTWARE.Migrations;
using Microsoft.VisualBasic;

namespace API_PROGRAMACION_DE_SOFTWARE.DAOs
{
    public class LoanDAO : ILoanDAO
    {
        private readonly SQLServerConfiguration _connectionString;
        private readonly ILogger<LoanController> _logger;

        public LoanDAO(IOptions<SQLServerConfiguration> connectionString, ILogger<LoanController> logger)
        {
            _connectionString = connectionString.Value;
            _logger = logger;
        }

        protected SqlConnection Connection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<List<Loan>> ListLoans()
        {
            List<Loan> result = new List<Loan>();

            try
            {
                using var db = Connection();
                IEnumerable<Loan> lista = await db.QueryAsync<Loan>(LoanQueries.listLoans, new { });
                _logger.LogInformation("Consulta exitosa de préstamos en SQL Server");
                return lista.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al leer los préstamos en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }

            return result;
        }

        public async Task<Loan> GetLoan(int loanId)
        {
            try
            {
                using var db = Connection();
                var loan = await db.QueryFirstOrDefaultAsync<Loan>(LoanQueries.getLoan, new { LoanId = loanId });
                _logger.LogInformation($"Búsqueda exitosa del préstamo con ID: {loanId} en SQL Server");

                if (loan == null)
                {
                    throw new InvalidOperationException($"No se encontró un préstamo con el ID: {loanId}");
                }
                return loan;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar el préstamo con ID: {loanId} en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (buscar): {ex.Message}");
                throw;
            }
        }
        public async Task<List<Loan>> SearchLoansUser(int userId)
        {
            try
            {
                using var db = Connection();
                var reservation = await db.QueryAsync<Loan>(LoanQueries.searchLoansUser, new { UserId = userId });
                return reservation.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar los prestamos para el usuario con ID: {userId} en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (buscar): {ex.Message}");

            }
            return null;
        }

        public async Task<Boolean> CreateLoan(int reservationId, int userId)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(LoanQueries.createLoan, new
                {
                    UserId = userId,
                    ReservationId = reservationId,
                    Status = ConversorEnumInt.LoanStatusConver(LoanStatus.Active.ToString())
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al insertar el préstamo en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<Boolean> ReturnLoan(Loan loan)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(LoanQueries.returnLoan, new
                {
                    loan.LoanId,
                    loan.ReturnDate,
                    Status = ConversorEnumInt.LoanStatusConver(LoanStatus.Completed.ToString())
                });
                return result > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el préstamo en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<Boolean> CancelLoan(Loan loan)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(LoanQueries.cancelLoan, new
                {
                    loan.LoanId,
                    Status = ConversorEnumInt.LoanStatusConver(LoanStatus.Canceled.ToString())
                });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cancelar el préstamo en la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server: {ex.Message}");
            }
            return result > 0;
        }

        public async Task<bool> DeleteLoan(int loanId)
        {
            int result = 0;
            try
            {
                using var db = Connection();
                result = await db.ExecuteAsync(LoanQueries.deleteLoan, new { LoanId = loanId });
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el préstamo con ID: {loanId} de la base de datos SQL Server: {ex.Message}");
                Console.WriteLine($"Error SQL Server (eliminar): {ex.Message}");
                return false;
            }
        }
    }
}
