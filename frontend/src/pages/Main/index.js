import React, { Component } from "react";
import { Card } from "../../components/RoomCard";
import { IoAddCircle } from "react-icons/io5";
import "./style.css";

export class Main extends Component {
  constructor(props) {
    super(props);
    this.state = { salas: [] };
  }

  componentDidMount() {
    this.LoadRoomList();
  }

  LoadRoomList() {
    fetch("http://localhost:5001/salas/listall")
      .then((resp) => resp.json())
      .then((response) => {
        console.log(response);
        this.setState({ salas: response });
      });
  }

  CreateNewRoom() {
    fetch("http://localhost:5001/salas/create")
      .then((resp) => resp.json())
      .then((response) => {
        window.location.reload();
      });
  }

  render() {
    return (
      <div className="main-container">
        <div className="addnew">
          <button onClick={this.CreateNewRoom} className="addbutton">
            <IoAddCircle />
            Criar nova sala
          </button>
        </div>
        <h2>Salas disponiveis</h2>
        <div className="list-container">
          {this.state.salas.map((item) => (
            <Card roomId={item} func={this.LoadRoomList} />
          ))}
        </div>
      </div>
    );
  }
}
