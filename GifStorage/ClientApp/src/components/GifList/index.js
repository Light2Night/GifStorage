import React, { Component } from 'react';
import GifItem from './GifItem';
import styles from './style.module.css'
import AddGifForm from './AddGifForm';

export class GifList extends Component {
  constructor(props) {
    super(props);

    this.state = {
      gifs: [],
      loading: true,
      showAddGifForm: false
    };
  }

  componentDidMount() {
    this.reloadGifs();
  }

  reloadGifs = () => {
    this.getGifs();
  }

  render() {
    const { gifs, loading, showAddGifForm } = this.state;

    const parsedGifs = gifs.map(g => <GifItem key={g.id} gif={g} />);

    return (
      <>
        <div>
          <button className="btn btn-primary" onClick={this.addGifMenuSwitch}>Меню додавання GIF</button>
        </div>
        {showAddGifForm && <AddGifForm reloadGifs={this.reloadGifs} />}
        <div className={styles.window}>
          {loading && <p>Loading...</p>}
          <div className={styles.container}>
            {parsedGifs}
          </div>
        </div>
      </>
    );
  }

  addGifMenuSwitch = () => {
    this.setState(oldState => {
      return {
        showAddGifForm: !oldState.showAddGifForm
      };
    });
  }

  getGifs = async () => {
    const response = await fetch('api/gif/get');
    const data = await response.json();
    this.setState({ gifs: data, loading: false });
  }
}
