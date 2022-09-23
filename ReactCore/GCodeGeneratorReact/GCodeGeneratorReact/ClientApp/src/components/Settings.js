import React, { Component } from 'react'
import Toast from 'react-bootstrap/Toast'
import './settings.css'

export class Settings extends Component {
	constructor(props) {
		super(props)
		this.state = {
			preScript: '',
			postScript: '',
			fullRoundSteps: 0,
			stepsPermm: 0,
			xInversion: false,
			yInversion: false,
			isChanged: false, // not server model
			showSavedToast: false, // not server model
		}
	}

	componentDidMount() {
		this.populateData()
	}

	handleSave = async () => {
		const rawResponse = await fetch('api/settings', {
			method: 'POST',
			headers: {
				Accept: 'application/json',
				'Content-Type': 'application/json',
			},
			body: JSON.stringify(this.state),
		})
		this.setState({ isChanged: false, showSavedToast: true })
	}

	handleInputChange = (event) => {
		const target = event.target
		const value = target.type === 'checkbox' ? target.checked : target.value
		const name = target.name
		this.setState({
			[name]: value,
			isChanged: true,
		})
	}

	render() {
		return (
			<div className='settings'>
				<div className='settings__textarea mb-3'>
					<div class="form-group col-6">
						<label>Pre script</label>
						<textarea
							class="form-control"
							name="preScript"
							onChange={this.handleInputChange}
							value={this.state.preScript}
							rows="3"
						></textarea>
					</div>
					<div class="form-group col-6">
						<label>postScript</label>
						<textarea
							class="form-control"
							name="postScript"
							onChange={this.handleInputChange}
							rows="3"
							value={this.state.postScript}
						></textarea>
					</div>
				</div>

				<div className='settings__inputs col-6 mb-3'>
					<div className="input-group ">
						<input
							onChange={this.handleInputChange}
							type="number"
							name="fullRoundSteps"
							value={this.state.fullRoundSteps}
							className="form-control"
						/>
					</div>
					<span className="settings__inputs-name">Steps per full round</span>
				</div>

				<div className='settings__inputs col-6 mb-3'>
					<div className="input-group">
						<input
							onChange={this.handleInputChange}
							type="number"
							name="stepsPermm"
							value={this.state.stepsPermm}
							className="form-control"
						/>
					</div>
					<span className="settings__inputs-name">Steps per mm</span>
				</div>

				<div className='settings__radio mb-3'><div class="form-check form-switch">
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
					</div></div>

				<button
					type="button"
					onClick={this.handleSave}
					className="btn btn-success"
					disabled={!this.state.isChanged}
				>
					Save settings
				</button>

				<div className='settings__toast'>
					<Toast
						onClose={() => this.setState({ showSavedToast: false })}
						show={this.state.showSavedToast}
						delay={3000}
					>
						<Toast.Header>
							<strong className="me-auto">Generator</strong>
						</Toast.Header>
						<Toast.Body>Settings saved</Toast.Body>
					</Toast>
				</div>
			</div>
		)
	}

	async populateData() {
		const response = await fetch('api/settings')
		const data = await response.json()
		this.setState(data)
	}
}
