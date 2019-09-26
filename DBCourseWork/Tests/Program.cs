using DataLayer.Entities;
using DataLayer.Implementation;
using System;
using System.Threading.Tasks;

namespace Tests {
    class Program {
        static UserRepository userRepository = new UserRepository();
        static GroupRepository groupRepository = new GroupRepository();
        static PaymentRepository paymentRepository = new PaymentRepository();
        static ExpenseRepository expenseRepository = new ExpenseRepository();
        static async Task Main(string[] args) {

        }

        static async Task TestUserRepository() {
            Console.WriteLine("Testing users");
            decimal balance = await userRepository.GetBalanceAsync(2, 3, 1);
            Console.WriteLine($"balance: {balance}");
        }

        static async Task TestGroupRepository() {
            Console.WriteLine("Testing groups");

            Console.WriteLine("Add user to group");
            await groupRepository.AddUserAsync(2, 5);
            foreach (var i in await groupRepository.GetUsersAsync(2))
                Console.WriteLine($"\tid: {i.Id}\tname: {i.Name}");

            Console.WriteLine("Remove user");
            await groupRepository.RemoveUserAsync(2, 5);
            foreach (var i in await groupRepository.GetUsersAsync(2))
                Console.WriteLine($"\tid: {i.Id}\tname: {i.Name}");

            Console.WriteLine("Add new group");
            var group = await groupRepository.CreateAsync(new Group() { Name = "Test groupe" });
            group = await groupRepository.GetByIdAsync(group.Id);
            Console.WriteLine($"\tid: {group.Id}\tname: {group.Name}");

            group.Name = "Changed name";
            await groupRepository.UpdateAsync(group);
            group = await groupRepository.GetByIdAsync(group.Id);
            Console.WriteLine($"\tid: {group.Id}\tname: {group.Name}");

            if (await groupRepository.DeleteAsync(group.Id)) {
                if (await groupRepository.GetByIdAsync(group.Id) == null)
                    Console.WriteLine("Successfully deleted");
            } else {
                Console.WriteLine("**** NOT DELETED ***");
            }
        }
    }
}
