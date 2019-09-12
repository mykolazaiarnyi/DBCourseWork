create view expenses
as
	select eh.id, eh.[description], eh.[time], eh.group_id, eh.[user_id] as by_user_id, el.amount, el.[user_id] as for_user_id 
		from expenses_header as eh join expenses_line as el
		on eh.id = el.expense_id