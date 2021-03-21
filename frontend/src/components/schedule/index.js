import React, { Component } from "react";
import { IoTrash, IoPencil } from "react-icons/io5";
import "./style.css";

export class Schedule extends Component {
  UpdateSchedule(roomId, id, props) {
    console.log(this.props.asd);
    this.props.asd.push(`/agendar/${roomId}/${id}`);
  }

  DeleteSchedule(id) {
    if (window.confirm("Deseja realmente apagar esse horario?")) {
      fetch(`http://localhost:5001/agendamento/delete?id=${id}`, {
        method: "DELETE",
      })
        .then((resp) => resp.json())
        .then((response) => {
          console.log(response);
          window.location.reload();
        });
    }
  }

  render() {
    return (
      <div className="schedule">
        {this.props.text}
        <button
          onClick={() => this.DeleteSchedule(this.props.id)}
          className="actionButton"
        >
          <IoTrash />
        </button>
        <button
          onClick={() => this.UpdateSchedule(this.props.roomId, this.props.id)}
          className="actionButton"
        >
          <IoPencil />
        </button>
      </div>
    );
  }
}
