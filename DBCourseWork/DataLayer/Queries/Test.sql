insert into users([name]) 
	values (N'Mykola'), (N'Oleh'), (N'Andrii'), (N'Nazar')
select * from users

insert into groups([name]) values (N'Za ekzamen'), (N'Picnic')

insert into user_groups ([user_id], group_id) values (2, 1), (3, 1), (4, 1)

select * from get_users_of_group(1, 3)
select * from get_users_of_group(2)

insert into user_groups ([user_id], group_id) values (1, 2), (4, 2)

select * from get_groups_of_user(1)
select * from get_groups_of_user(4)

delete from expenses_header

insert into expenses_total([description], group_id, [by_user_id], amount) values (N'Za BGD', 1, 2, 800)
insert into expenses_total([description], group_id, [by_user_id], amount) values (N'Za vse khoroshe', 1, 3, 200)

select * from expenses_total
select * from expenses
select * from expenses_header
select * from expenses_line

select * from user_groups order by group_id

select * from payments
insert into payments ([description], group_id, [user_id], to_user_id, amount) values (N'Za labky', 1, 4, 2, 100)
set transaction isolation level serializable; begin transaction; select dbo.get_users_balance(2, 3, 1); commit