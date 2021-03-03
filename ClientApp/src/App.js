import React from 'react';
import { Route } from 'react-router-dom';
import Home  from './components/Home.js';
import './custom.css'

const App = () => {
    return (
        <Route exact path='/'>
          <Home/>
        </Route>
    );
}

export default App; 
