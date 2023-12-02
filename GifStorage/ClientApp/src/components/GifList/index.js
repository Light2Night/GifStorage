import React, { Component } from 'react';
import GifItem from './GifItem';
import styles from './style.module.css'
import AddGif from '../AddGif';
import { getGifsAsync } from '../../api';

export class GifList extends Component {
  constructor(props) {
    super(props);

    this.state = {
      gifs: [],
      loading: true
    };
  }

  componentDidMount() {
    this.reloadGifs();
  }

  reloadGifs = () => {
    this.getGifs();
  }

  render() {
    const { gifs, loading } = this.state;

    const parsedGifs = gifs.map(g => <GifItem key={g.id} gif={g} />);

    return (
      <>
        <div className={styles.window}>
          {loading && <p>Loading...</p>}
          <div className={styles.container}>
            {parsedGifs}
          </div>
        </div>
      </>
    );
  }

  getGifs = async () => {
    const data = await getGifsAsync();
    this.setState({ gifs: data, loading: false });
  }
}
