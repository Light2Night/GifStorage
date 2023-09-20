import React, { Component } from 'react';

export class GifList extends Component {
  constructor(props) {
    super(props);

    this.state = {
      gifs: [],
      loading: true
    };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  render() {
    const { gifs, loading } = this.state;

    const parsedGifs = gifs.map(g => <img id={g.id} src={g.url} alt='gif'></img>);

    return (
      <div>
        {loading && <p>Loading...</p>}
        {parsedGifs}
      </div>
    );
  }

  populateWeatherData = async () => {
    const response = await fetch('api/gif');
    const data = await response.json();
    this.setState({ gifs: data, loading: false });
  }
}
