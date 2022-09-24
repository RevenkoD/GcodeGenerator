import React, { Component } from 'react'
import './generator.css'
import image from '../image/image62.png'
import Toast from 'react-bootstrap/Toast'

export class Generator extends Component {
  constructor(props) {
    super(props)
    let defaultData = {
      length: 500,
      diameter: 85,
      angle: 45,
      speedOfFiber: 11000,
      fiberWidth: 6,
      countOfExtraLoop: 1,
      countOfLoops: 0,
      countOfFullLoops: 1,
      useLoops: 0,
      showGeneratedToast: false,
      showPresetSaveToast: false,
      showPresetLoadToast: false,
    }
    let data = window.localStorage.getItem('my-key')
    if (data != null) {
      data = JSON.parse(data)
      this.state = data
    } else this.state = defaultData
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
    const rawResponse = await fetch('api/generate', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.state),
    })
    const content = await rawResponse.json()
    if (content == 1) this.setState({ showGeneratedToast: true })
    console.log(content)
  }

  handleSavePreset = async () => {
    const rawResponse = await fetch('api/generate/preset', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.state),
    })
    const content = await rawResponse.json()
    if (content == 1) this.setState({ showPresetSaveToast: true })
    console.log(content)
  }

  handleLoadPreset = async () => {
    const rawResponse = await fetch('api/generate/preset')
    const content = await rawResponse.json()
    this.setState({
      ...content,
      showPresetLoadToast: true,
    })
    console.log(content)
  }

  componentWillUnmount = () => {
    window.localStorage.setItem('my-key', JSON.stringify(this.state))
  }

  render() {
    return (
      <div className="generator">
        <div className="generator__loadsave">
          <button
            type="button"
            class="btn btn-primary col-5 generator__loadsave-btn"
            onClick={this.handleLoadPreset}
          >
            Load preset
          </button>
          <button
            type="button"
            class="btn btn-secondary col-5"
            onClick={this.handleSavePreset}
          >
            Save preset
          </button>
        </div>
        <div className="generator__body">
          <div className="generator__body-inputs col-7">
            <div className="generator__inputs  mb-3 ">
              <div className="input-group">
                <input
                  onChange={this.handleInputChange}
                  type="number"
                  name="length"
                  value={this.state.length}
                  className="form-control"
                />
                <span className="input-group-text">mm</span>
              </div>
              <span className="generator__inputs-name">Length</span>
            </div>
            <div className="generator__inputs mb-3">
              <div className="input-group">
                <input
                  onChange={this.handleInputChange}
                  type="number"
                  name="diameter"
                  value={this.state.diameter}
                  className="form-control"
                />
                <span className="input-group-text">mm</span>
              </div>
              <span className="generator__inputs-name">Diameter</span>
            </div>

            <div className="generator__inputs mb-3">
              <div className="input-group">
                <input
                  onChange={this.handleInputChange}
                  type="number"
                  name="angle"
                  value={this.state.angle}
                  className="form-control"
                />
                <span className="input-group-text">deg</span>
              </div>
              <span className="generator__inputs-name">Angle</span>
            </div>
            <div className="generator__inputs mb-3">
              <div className="input-group">
                <input
                  onChange={this.handleInputChange}
                  type="number"
                  name="speedOfFiber"
                  value={this.state.speedOfFiber}
                  className="form-control"
                />
                <span className="input-group-text">mm/min</span>
              </div>
              <span className="generator__inputs-name">
                Speed&nbsp;of&nbsp;fiber
              </span>
            </div>

            <div className="generator__inputs mb-3">
              <div className="input-group">
                <input
                  onChange={this.handleInputChange}
                  type="number"
                  name="fiberWidth"
                  value={this.state.fiberWidth}
                  className="form-control"
                />
                <span className="input-group-text">mm</span>
              </div>
              <span className="generator__inputs-name">Fiber&nbsp;width</span>
            </div>

            <div className="generator__inputs mb-3">
              <div className="input-group ">
                <input
                  onChange={this.handleInputChange}
                  type="number"
                  name="countOfExtraLoop"
                  value={this.state.countOfExtraLoop}
                  className="form-control"
                />
              </div>
              <span className="generator__inputs-name">
                Count Of Extra Loops
              </span>
            </div>

            <div className="generator__radio mb-3">
              <div className="input-group">
                <div className="generator__radio-inputs">
                  <input
                    class="form-check-input"
                    type="radio"
                    name="useLoops"
                    value={1}
                    checked={this.state.useLoops == 1}
                    onChange={this.handleInputChange}
                  />
                  <input
                    disabled={this.state.useLoops == 0}
                    onChange={this.handleInputChange}
                    type="number"
                    name="countOfLoops"
                    value={this.state.countOfLoops}
                    className="form-control generator__radio-inp"
                  />
                </div>
                <span className="input-group-text">Count of loops</span>
              </div>
            </div>

            <div className="generator__radio mb-3">
              <div className="input-group">
                <div className="generator__radio-inputs">
                  <input
                    class="form-check-input"
                    type="radio"
                    name="useLoops"
                    value={0}
                    onChange={this.handleInputChange}
                    checked={this.state.useLoops == 0}
                  />
                  <input
                    disabled={this.state.useLoops != 0}
                    onChange={this.handleInputChange}
                    type="number"
                    name="countOfFullLoops"
                    value={this.state.countOfFullLoops}
                    className="form-control"
                  />
                </div>
                <span className="input-group-text">Count of full loops</span>
              </div>
            </div>
          </div>
          <div className="generator__body-imgs col-5">
            <img src={image} style={{ visibility: 'hiden' }}></img>
          </div>
        </div>
        <div className="generator__gcode">
          <button
            type="button"
            onClick={this.handleGenerate}
            className="btn btn-success col-12"
          >
            Generate G-code
          </button>
        </div>
        <div className="settings__toast">
          <Toast
            onClose={() => this.setState({ showGeneratedToast: false })}
            show={this.state.showGeneratedToast}
            delay={2000}
            autohide
          >
            <Toast.Header>
              <strong className="me-auto">Generator</strong>
            </Toast.Header>
            <Toast.Body>Gcode generated</Toast.Body>
          </Toast>
          <Toast
            onClose={() => this.setState({ showPresetSaveToast: false })}
            show={this.state.showPresetSaveToast}
            delay={2000}
            autohide
          >
            <Toast.Header>
              <strong className="me-auto">Generator</strong>
            </Toast.Header>
            <Toast.Body>Preset saved</Toast.Body>
          </Toast>
          <Toast
            onClose={() => this.setState({ showPresetLoadToast: false })}
            show={this.state.showPresetLoadToast}
            delay={2000}
            autohide
          >
            <Toast.Header>
              <strong className="me-auto">Generator</strong>
            </Toast.Header>
            <Toast.Body>Preset loaded</Toast.Body>
          </Toast>
        </div>
      </div>
    )
  }
}
