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