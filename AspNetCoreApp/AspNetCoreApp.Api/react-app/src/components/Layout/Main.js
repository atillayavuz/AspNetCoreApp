import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import Todo from "../../container/Todo";
import Profile from "../../container/Profile";
import About from "../../container/About";

const Main = () => (
  <main>
    <Switch>
      <Route exact path="/" component={Todo} />
      <Route exact path="/Profile" component={Profile} />
      <Route exact path="/About" component={About} />
    </Switch>
  </main>
);
export default Main;
