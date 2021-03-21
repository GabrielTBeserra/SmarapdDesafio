import React, { Component } from "react";
import { Link } from "react-router-dom";
import "react-date-range/dist/styles.css"; // main style file
import "react-date-range/dist/theme/default.css"; // theme css file
import { Schedule } from "../../components/schedule";

import "./style.css";

export class Room extends Component {
  constructor(props) {
    super(props);
    this.state = {
      id: this.props.match.params.id,
      sala: {},
      ranges: [],
    };
  }

  componentDidMount() {
    this.LoadRoom();
  }

  LoadRoom() {
    fetch(`http://localhost:5001/salas/get?id=${this.state.id}`)
      .then((resp) => resp.json())
      .then((response) => {
        this.setState({ sala: response });
        this.CreateScheduleRange();
      });
  }

  CreateScheduleRange() {
    let scheduligns = this.state.sala.schedulings.map((asd) => {
      return {
        roomId: this.state.id,
        id: asd.id,
        startDate: new Date(asd.startTime),
        endDate: new Date(asd.endTime),
        title: asd.title,
        key: "selection",
      };
    });

    this.setState({ ranges: scheduligns });
  }

  FormatDate(dateIn) {
    var yyyy = dateIn.getFullYear();
    var mm = dateIn.getMonth() + 1;
    var dd = dateIn.getDate();
    return `${dd}/${mm}/${yyyy} ${dateIn.getHours()}:${dateIn.getMinutes()}`;
  }

  handleSelect(ranges) {
    console.log(ranges);
    // <DateRange ranges={this.state.ranges} onChange={this.handleSelect} />
  }

  render() {
    return (
      <div className="scheduleList">
        <h1>Horarios Ocupados pela Sala {this.state.id}</h1>
        <Link to={`/agendar/${this.state.id}`} className="CreateNewButton">
          Agendar
        </Link>
        <hr />
        {this.state.ranges.map((item) => (
          <>
            <Schedule
              text={`${this.FormatDate(item.startDate)} -> ${this.FormatDate(
                item.endDate
              )} (${item.title})`}
              id={item.id}
              roomId={item.roomId}
              asd={this.props.history}
            ></Schedule>
          </>
        ))}
      </div>
    );
  }
}
