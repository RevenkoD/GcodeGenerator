import React, { Component } from 'react'

export class Settings extends Component {
  static displayName = Settings.name

  constructor(props) {
    super(props)
    this.state = {
      preScript: '',
      postScript: '',
      fullRoundSteps: 0,
      stepsPermm: 0,
      xInversion: false,
      yInversion: false,
    }
  }

  componentDidMount() {
    this.populateData()
  }

  handleSave = async () => {}

  handleInputChange = (event) => {
    const target = event.target
    const value = target.type === 'checkbox' ? target.checked : target.value
    const name = target.name
    this.setState({
      [name]: value,
    })
  }

  render() {
    return (
      <div>
        <div class="form-group">
          <label>Pre script</label>
          <textarea
            class="form-control"
            name="preScript"
            onChange={this.handleInputChange}
            value={this.state.preScript}
            rows="3"
          ></textarea>
        </div>
        <div class="form-group">
          <label>postScript</label>
          <textarea
            class="form-control"
            name="postScript"
            onChange={this.handleInputChange}
            rows="3"
            value={this.state.postScript}
          ></textarea>
        </div>

        <div className="input-group mb-3">
          <span className="input-group-text">Steps per full round</span>
          <input
            onChange={this.handleInputChange}
            type="number"
            name="fullRoundSteps"
            value={this.state.fullRoundSteps}
            className="form-control"
          />
        </div>

        <div className="input-group mb-3">
          <span className="input-group-text">Steps per mm</span>
          <input
            onChange={this.handleInputChange}
            type="number"
            name="stepsPermm"
            value={this.state.stepsPermm}
            className="form-control"
          />
        </div>

        <div class="form-check form-switch">
          <input
            class="form-check-input"
            type="checkbox"
            role="switch"
            checked={this.state.yInversion}
            name="yInversion"
            onChange={this.handleInputChange}
          />
          <label class="form-check-label">Rotation axis inversion</label>
        </div>
        <div class="form-check form-switch">
          <input
            class="form-check-input"
            type="checkbox"
            role="switch"
            checked={this.state.xInversion}
            name="xInversion"
            onChange={this.handleInputChange}
          />
          <label class="form-check-label">Linear axis inversion</label>
        </div>
        <button
          type="button"
          onClick={this.handleSave}
          className="btn btn-success"
        >
          Save settings
        </button>
      </div>
    )
  }

  async populateData() {
    const response = await fetch('api/settings')
    const data = await response.json()
    this.setState(data)
  }
}
