import React, { Component } from 'react';
import GifItem from './GifItem';
import styles from './style.module.css'

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

    const parsedGifs = gifs.map(g => <GifItem gif={g} />);

    return (
      <div className={styles.window}>
        {loading && <p>Loading...</p>}
        <div className={styles.container}>
          {parsedGifs}
        </div>
      </div>
    );
  }

  populateWeatherData = async () => {
    const response = await fetch('api/gif/get');
    const data = await response.json();
    this.setState({ gifs: data, loading: false });
  }
}
