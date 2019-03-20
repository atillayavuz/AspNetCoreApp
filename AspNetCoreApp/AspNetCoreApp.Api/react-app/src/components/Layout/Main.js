import React from "react";
import { Switch, Route } from "react-router-dom";
import Todo from "../../container/Todo";
import Profile from "../../container/Profile";
import About from "../../container/About";
import TodoDetails from "../../container/TodoDetails";

const Main = () => (
  <main>
    <Switch>
      <Route exact path="/" component={Todo} />
      <Route exact path="/Profile" component={Profile} />
      <Route exact path="/About" component={About} />
      <Route exact path="/Todo/:id" component={TodoDetails} />
    </Switch>
  </main>
);
export default Main;
