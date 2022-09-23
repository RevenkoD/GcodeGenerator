import React, { Component } from 'react'

export class Generator extends Component {
  constructor(props) {
    super(props)
    this.state = {
      length: 0,
      diameter: 0,
      angle: 0,
      speedOfFiber: 0,
      fiberWidth: 0,
      countOfExtraLoop: 0,
      countOfLoops: 0,
      countOfFullLoops: 0,
      preScript: 'G21\nG91\n',
      postScript: '',
      useLoops: 0,
    }
  }

  handleInputChange = (event) => {
    const target = event.target
    const value = target.type === 'checkbox' ? target.checked : target.value
    const name = target.name
    this.setState({
      [name]: value,
    })
  }

  handleGenerate = async () => {
    console.log(this.state)
    const rawResponse = await fetch('api/generate', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.state),
    })
    const content = await rawResponse.json()
    console.log(content)
  }

  render() {
    return (
      <div>
        <button type="button" class="btn btn-primary">
          Load preset
        </button>
        <button type="button" class="btn btn-secondary">
          Save preset
        </button>

        <div className="input-group mb-3">
          <span className="input-group-text">Length</span>
          <input
            onChange={this.handleInputChange}
            type="number"
            name="length"
            value={this.state.length}
            className="form-control"
          />
          <span className="input-group-text">mm</span>
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Diameter</span>
          <input
            onChange={this.handleInputChange}
            type="number"
            name="diameter"
            value={this.state.diameter}
            className="form-control"
          />
          <span className="input-group-text">mm</span>
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Angle</span>
          <input
            onChange={this.handleInputChange}
            type="number"
            name="angle"
            value={this.state.angle}
            className="form-control"
          />
          <span className="input-group-text">deg</span>
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Speed of fiber</span>
          <input
            onChange={this.handleInputChange}
            type="number"
            name="speedOfFiber"
            value={this.state.speedOfFiber}
            className="form-control"
          />
          <span className="input-group-text">mm/min</span>
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Fiber width</span>
          <input
            onChange={this.handleInputChange}
            type="number"
            name="fiberWidth"
            value={this.state.fiberWidth}
            className="form-control"
          />
          <span className="input-group-text">mm</span>
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">CountOfExtraLoops</span>
          <input
            onChange={this.handleInputChange}
            type="number"
            name="countOfExtraLoop"
            value={this.state.countOfExtraLoop}
            className="form-control"
          />
        </div>

        <div className="input-group mb-3">
          <input
            class="form-check-input"
            type="radio"
            name="useLoops"
            value={1}
            checked={this.state.useLoops == 1}
            onChange={this.handleInputChange}
          />
          <span className="input-group-text">Count of loops</span>
          <input
            disabled={this.state.useLoops == 0}
            onChange={this.handleInputChange}
            type="number"
            name="countOfLoops"
            value={this.state.countOfLoops}
            className="form-control"
          />

          <input
            class="form-check-input"
            type="radio"
            name="useLoops"
            value={0}
            onChange={this.handleInputChange}
            checked={this.state.useLoops == 0}
          />
          <span className="input-group-text">Count of full loops</span>
          <input
            disabled={this.state.useLoops != 0}
            onChange={this.handleInputChange}
            type="number"
            name="countOfFullLoops"
            value={this.state.countOfFullLoops}
            className="form-control"
          />
        </div>

        <button
          type="button"
          onClick={this.handleGenerate}
          className="btn btn-success"
        >
          Generate G-code
        </button>
      </div>
    )
  }
}
