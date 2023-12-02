import React, { Component } from 'react';
import { createCategoryAsync } from '../../../api';

class AddGifForm extends Component {
  constructor(props) {
    super(props);

    this.state = {
      url: "",
      errorMessage: ""
    }
  }

  render() {
    const { url, errorMessage } = this.state;

    return (
      <form onSubmit={this.handleSubmit}>
        <div className="form-group">
          <label htmlFor="urlInput">Url</label>
          <input type="text" className="form-control" id="urlInput" value={url} onChange={this.handleInputChange} required />
          {errorMessage && <span className='text-danger'>{errorMessage}</span>}
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

  addGif = async () => {
    const { url } = this.state;

    const form = new FormData();
    form.append('url', url);

    try {
      createCategoryAsync(form);
      this.setState({ url: "" });
      this.props.reloadGifs();
    }
    catch (error) {
      this.setState({ errorMessage: "Помилка додавання, можливо такий елемент уже є у базі даних" });
    }
  }
}

export default AddGifForm;