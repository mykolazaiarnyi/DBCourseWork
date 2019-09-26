import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { login: "", toBeRedirected: false };
        this.handleClick = this.handleClick.bind(this)
    }

    handleClick() {
        this.setState({ toBeRedirected: true })
    }

    render() {
        if (this.state.toBeRedirected) {
            return <Redirect to={{
                pathname: "/fetch-data", state: { str: "Some value" }
            }} />
        }
        return (
            <div>
                <h1>Log in</h1>
                <input type="text" id="userName" /> <br /> <br />
                <button className="btn btn-primary" onClick={this.handleClick}>Log in</button>
            </div>
        );
    }
}
