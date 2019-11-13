create or alter trigger create_expense on expenses_total
instead of insert
as
begin
	declare @number_of_members int
	select @number_of_members = count(*) from user_groups as ug
		join inserted as i on ug.group_id = i.group_id
		
	declare @group_id int
	declare @amount int
	declare @by_user_id int
	select @group_id = i.group_id, @amount = i.amount, @by_user_id = i.by_user_id from inserted as i

	insert into expenses_header ([description], [time], group_id, [user_id]) 
		select i.[description], cast(GETDATE() as smalldatetime) , @group_id, i.[by_user_id] from inserted as i
	declare @eh_id int
	select @eh_id = SCOPE_IDENTITY()

	insert into expenses_line ([user_id], amount, expense_id) select u.id, @amount / @number_of_members, @eh_id from get_users_of_group(@group_id, @by_user_id) as u
end