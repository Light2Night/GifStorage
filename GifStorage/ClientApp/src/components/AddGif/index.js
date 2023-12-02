import React, { Component } from 'react';
import { createCategoryAsync } from '../../api';
import { Formik, Form, Field, ErrorMessage } from 'formik';

class AddGif extends Component {
  constructor(props) {
    super(props);

    this.state = {
      errorMessage: ""
    }
  }

  render() {
    const { errorMessage } = this.state;

    const formBodyFunction = ({ isSubmitting }) => (
      <Form className='container-fluid' style={{ paddingBottom: "1rem" }}>
        <div className="form-group">
          <label htmlFor="urlInput">URL</label>
          <Field name='url' type='text' className="form-control" id="urlInput" />
          <ErrorMessage name="url" component="div" className='text-danger' />
        </div>
        <button type="submit" disabled={isSubmitting} className="btn btn-primary"
          style={{ marginTop: "1rem" }}>Додати
        </button>
        {errorMessage && <p className='text-danger'>{errorMessage}</p>}
      </Form>
    );

    return (
      <Formik
        initialValues={{
          url: ''
        }}

        validate={this.validateData}
        onSubmit={this.submitHandler}
      >
        {formBodyFunction}
      </Formik>
    );
  }

  setErrorMessage = (errorMessage) => this.setState({ errorMessage });

  validateData = (values) => {
    const errors = {};
    const { url } = values;

    if (!url)
      errors.url = 'Введіть URL-адресу GIF';

    return errors;
  }

  submitHandler = async (values, { setSubmitting }) => {
    const { url } = values;

    const form = new FormData();
    form.append('url', url);

    try {
      await createCategoryAsync(form);
      this.setErrorMessage('');
      values.url = '';
    }
    catch (error) {
      this.setErrorMessage("Помилка додавання, можливо такий елемент уже є у базі даних");
    }

    setSubmitting(false);
  }
}

export default AddGif;