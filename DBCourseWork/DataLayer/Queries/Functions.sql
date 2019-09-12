create function get_groups_of_user (@id int)
returns table
as
return (
	select g.id, g.[name] from user_groups as ug 
		join groups as g
		on ug.group_id = g.id
		where ug.[user_id] = @id
)
go

create function get_users_of_group (@id int)
returns table
as
return (
	select u.id, u.[name] from user_groups as ug
		join users as u
		on ug.[user_id] = u.id
		where ug.group_id = @id
)
go

create function get_users_balance (@user_id_1 int, @user_id_2 int, @group_id int)
returns money
as
begin
	declare @number_of_members int
	select @number_of_members = count(*) from user_groups as ug
		where ug.group_id = @group_id

	declare @spent_by_1 money
	select @spent_by_1 = (sum(e.amount) / (@number_of_members - 1)) from expenses as e 
		where e.group_id = @group_id
			and e.[user_id] = @user_id_1

	declare @spent_by_2 money
	select @spent_by_2 = (sum(e.amount) / (@number_of_members - 1)) from expenses as e 
		where e.group_id = @group_id 
			and e.[user_id] = @user_id_2

	declare @payed_by_1 money
	select @payed_by_1 = sum(p.amount) from payments as p
		where p.[user_id] = @user_id_1 
			and p.to_user_id = @user_id_2 
			and p.group_id = @group_id
			and p.confirmed = 1

	declare @payed_by_2 money
	select @payed_by_2 = sum(p.amount) from payments as p
		where p.[user_id] = @user_id_2 
			and p.to_user_id = @user_id_1 
			and p.group_id = @group_id
			and p.confirmed = 1

	return (@spent_by_1 - @payed_by_2) - (@spent_by_2 - @payed_by_1)
end