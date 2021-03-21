import React, { Component } from "react";
import "react-date-range/dist/styles.css";
import "react-date-range/dist/theme/default.css";
import "./style.css";

export class NewSchedule extends Component {
  constructor(props) {
    super(props);
    this.state = {
      id: this.props.match.params.id,
      titulo: "",
      horaInicial: new Date(),
      horaFinal: new Date(),
      dataInicial: new Date(),
      dataFinal: new Date(),
      mensagem: "",
      scheduleid: this.props.match.params.scheduleid,
      buttonTitle: "Agendar",
    };

    this.handleFianlHour = this.handleFianlHour.bind(this);
    this.handleFinalDate = this.handleFinalDate.bind(this);
    this.handleInitialDate = this.handleInitialDate.bind(this);
    this.handleInitialHour = this.handleInitialHour.bind(this);
    this.handleTitle = this.handleTitle.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount() {
    this.LoadRoom();
  }

  LoadRoom() {
    if (this.state.scheduleid) {
      fetch(
        `http://localhost:5001/agendamento/get?id=${this.state.scheduleid}&roomid=${this.state.id}`
      )
        .then((resp) => resp.json())
        .then((response) => {
          let start = new Date(response.startTime);
          let end = new Date(response.endTime);
          this.setState({
            sala: response,
            titulo: response.title,
            horaInicial: start.toISOString().substring(11, 16),
            horaFinal: end.toISOString().substring(11, 16),
            dataInicial: start.toISOString().substr(0, 10),
            dataFinal: end.toISOString().substr(0, 10),
            buttonTitle: "Atualizar",
          });
        });
    } else {
      fetch(
        `http://localhost:5001/salas/get?id=${this.state.id}&roomid=${this.state.id}`
      )
        .then((resp) => resp.json())
        .then((response) => {
          this.setState({ sala: response });
        });
    }
  }

  handleInitialDate(event) {
    this.setState({ dataInicial: event.target.value });
  }

  handleInitialHour(event) {
    this.setState({ horaInicial: event.target.value });
  }

  handleFianlHour(event) {
    this.setState({ horaFinal: event.target.value });
  }

  handleFinalDate(event) {
    this.setState({ dataFinal: event.target.value });
  }

  handleTitle(event) {
    this.setState({ titulo: event.target.value });
  }

  handleSubmit(event) {
    let dataInicial = new Date(this.state.dataInicial);
    let splitHoraInicial = this.state.horaInicial.toString().split(":");
    dataInicial.setHours(splitHoraInicial[0], splitHoraInicial[1]);

    let dataFinal = new Date(this.state.dataFinal);
    let splitHoraFinal = this.state.horaFinal.toString().split(":");
    dataFinal.setHours(splitHoraFinal[0], splitHoraFinal[1]);

    if (this.state.scheduleid) {
      dataInicial.setDate(dataInicial.getDate() + 1);
      dataFinal.setDate(dataFinal.getDate() + 1);

      fetch("http://localhost:5001/agendamento/update", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          id: parseInt(this.state.scheduleid),
          roomId: parseInt(this.state.id),
          title: this.state.titulo,
          startTime: dataInicial.toISOString(),
          endTime: dataFinal.toISOString(),
        }),
      })
        .then((response) => response.json())
        .then((resp) => {
          this.setState({ mensagem: resp.message });
          if (resp.type === "SUCESS") {
            this.props.history.push(`/sala/${this.state.id}`);
          }
        });
    } else {
      fetch("http://localhost:5001/agendamento/insert", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          roomId: parseInt(this.state.id),
          title: this.state.titulo,
          startTime: dataInicial.toISOString(),
          endTime: dataFinal.toISOString(),
        }),
      })
        .then((response) => response.json())
        .then((resp) => {
          this.setState({ mensagem: resp.message });
          if (resp.type === "SUCESS") {
            this.props.history.push(`/sala/${this.state.id}`);
          }
        });
    }

    event.preventDefault();
  }

  render() {
    return (
      <div className="container">
        <form onSubmit={this.handleSubmit} className="addform">
          <label className="schedulelabel">Titulo:</label>
          <input
            type="text"
            value={this.state.titulo}
            onChange={this.handleTitle}
            className="scheduleinput"
          />

          <label className="schedulelabel">Data Inicial:</label>
          <input
            type="date"
            value={this.state.dataInicial}
            onChange={this.handleInitialDate}
            className="scheduleinput"
          />
          <label className="schedulelabel">Hora Inicial:</label>
          <input
            type="time"
            value={this.state.horaInicial}
            onChange={this.handleInitialHour}
            className="scheduleinput"
          />

          <label className="schedulelabel">Data Final:</label>
          <input
            type="date"
            value={this.state.dataFinal}
            onChange={this.handleFinalDate}
            className="scheduleinput"
          />
          <label className="schedulelabel">Hora Final:</label>
          <input
            type="time"
            value={this.state.horaFinal}
            onChange={this.handleFianlHour}
            className="scheduleinput"
          />

          <input
            className="schedulebutton"
            type="submit"
            value={this.state.buttonTitle}
          />
          <p>{this.state.mensagem}</p>
        </form>
      </div>
    );
  }
}
