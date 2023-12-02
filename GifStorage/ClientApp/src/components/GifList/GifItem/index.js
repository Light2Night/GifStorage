import React, { Component } from 'react';
import styles from './style.module.css';
import CopyToClipboard from 'react-copy-to-clipboard';
import { invalidImageUrl } from '../../../api';

class GifItem extends Component {
  constructor(props) {
    super(props);

    const { id, url } = props.gif;

    this.id = id;
    this.url = url;

    this.state = {
      copied: false
    }
  }

  render() {
    return (
      <div className={styles.container}>
        <CopyToClipboard text={this.url} onCopy={this.copyHandler}>
          <img id={this.id} src={this.url} alt='gif' onError={this.imageErrorHandler} className={styles.image}></img>
        </CopyToClipboard>
      </div>
    );
  }

  copyHandler = (text, result) => {
    // console.log(text, result);
  }

  imageErrorHandler = (e) => {
    e.target.src = invalidImageUrl;
  }
}

export default GifItem;