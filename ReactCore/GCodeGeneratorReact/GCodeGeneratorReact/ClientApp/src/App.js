import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Settings } from './components/Settings';
import { Generator } from './components/Generator';

import './custom.css'

export default class App extends Component {
  render () {
    return (
      <Layout>
        <Route exact path='/' component={Generator} />
        <Route path='/settings' component={Settings} />
      </Layout>
    );
  }
}
