import { BrowserRouter, Route, Switch } from "react-router-dom";
import "./App.css";
import { Main } from "./pages/Main";
import { Room } from "./pages/Room";
import { NewSchedule } from "./pages/NewSchedule";
import { Header } from "./components/Header";

function App() {
  return (
    <BrowserRouter>
      <div>
        <Header />
        <Switch>
          <Route path="/" component={Main} exact />
          <Route path="/sala/:id" component={Room} />
          <Route path="/agendar/:id/:scheduleid?" component={NewSchedule} />
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
