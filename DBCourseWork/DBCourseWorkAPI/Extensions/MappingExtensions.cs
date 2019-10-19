using AutoMapper;
using DataLayer.Abstraction;
using DataLayer.Entities;
using DBCourseWorkAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBCourseWorkAPI.Extensions {
    public static class MappingExtensions {
        public async static Task<PaymentDto> PaymentToDtoAsync(this IMapper mapper, Payment payment, IUserRepository userRepository) {
            return new PaymentDto() {
                Id = payment.Id,
                Description = payment.Description,
                Time = payment.Time,
                Amount = payment.Amount,
                ByUser = (await userRepository.GetByIdAsync(payment.ByUserId)).Name,
                ForUser = (await userRepository.GetByIdAsync(payment.ForUserId)).Name,
                Confirmed = payment.Confirmed
            };
        }

        public async static Task<Payment> DtoToPaymentAsync(this IMapper mapper, PaymentDto paymentDto, IUserRepository userRepository) {
            return new Payment() {
                Id = paymentDto.Id,
                Description = paymentDto.Description,
                Time = paymentDto.Time,
                Amount = paymentDto.Amount,
                ByUserId = (await userRepository.GetByNameAsync(paymentDto.ByUser)).Id,
                ForUserId = (await userRepository.GetByNameAsync(paymentDto.ForUser)).Id,
                Confirmed = paymentDto.Confirmed
            };
        }

        public async static Task<ExpenseDto> ExpenseToDtoAsync(this IMapper mapper, Expense expense, IUserRepository userRepository) {
            return new ExpenseDto() {
                Amount = expense.Amount,
                Description = expense.Description,
                Time = expense.Time,
                ByUserName = (await userRepository.GetByIdAsync(expense.ByUserId)).Name
            };
        }

        public async static Task<Expense> DtoToExpenseAsync(this IMapper mapper, ExpenseDto expenseDto, IUserRepository userRepository) {
            return new Expense() {
                Amount = expenseDto.Amount,
                Description = expenseDto.Description,
                Time = expenseDto.Time,
                ByUserId = (await userRepository.GetByNameAsync(expenseDto.ByUserName)).Id
            };
        }
    }
}
