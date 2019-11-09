export class User {
    id: number;
    name: string;
}

export class UserWithBalance extends User {
    balance: number;
}

export class Group {
    id: number;
    name: string;
}

export class Expense {
    description: string;
    time: string;
    byUserName: string;
    amount: number;
}

export class Payment {
    id: number;
    description: string;
    time: string;
    byUser: string;
    amount: number;
    forUser: string;
    confirmed: boolean;
}
