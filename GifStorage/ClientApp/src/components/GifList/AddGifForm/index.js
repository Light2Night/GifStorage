import React, { Component } from 'react';

class AddGifForm extends Component {
  constructor(props) {
    super(props);

    this.state = {
      url: ""
    }
  }

  render() {
    const { url } = this.state;

    return (
      <form onSubmit={this.handleSubmit}>
        <div className="form-group">
          <label htmlFor="urlInput">Url</label>
          <input type="text" className="form-control" id="urlInput" value={url} onChange={this.handleInputChange} />
        </div>
        <br />
        <button type="submit" className="btn btn-primary">Додати</button>
      </form>
    );
  }

  handleInputChange = (event) => {
    this.setState({ url: event.target.value });
  };

  handleSubmit = (event) => {
    event.preventDefault();
    this.addGif();
  };

  addGif = () => {
    const { url } = this.state;

    const data = { url };

    fetch('api/gif/add', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    })
      .then(() => {
        this.setState({ url: "" });
        this.props.reloadGifs();
        console.log('Успішно додано') // Видалити в майбутньому
      })
      .catch(error => {
        console.error('Помилка додавання:', error); // Видалити в майбутньому
      });
  }
}

export default AddGifForm;