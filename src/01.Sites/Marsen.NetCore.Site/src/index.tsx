import * as React from "react";
import * as ReactDOM from "react-dom";
import { Hello } from ".\\components\\Hello";
import { Clock } from ".\\components\\Clock";
import { User } from ".\\classes\\User";

ReactDOM.render(
    <Hello compiler="TypeScript" framework="React"/>,
    document.getElementById("example")
);

function formatName(user: User) {
    return user.lastName + ' ' + user.firstName;
}

const user = new User('Mark', 'Lin');

const element = <h1>你好, {formatName(user)}!</h1>;

ReactDOM.render(
    <Clock/>, document.getElementById('root')
);