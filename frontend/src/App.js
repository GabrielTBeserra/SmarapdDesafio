import { BrowserRouter, Route, Switch } from "react-router-dom";
import "./App.css";
import { Main } from "./pages/Main";
import { Room } from "./pages/Room";
import { NewSchedule } from "./pages/NewSchedule";

function App() {
  return (
    <BrowserRouter>
      <div>
        <Switch>
          <Route path="/" component={Main} exact />
          <Route path="/sala/:id" component={Room} />
          <Route path="/agendar/:id" component={NewSchedule} />
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
