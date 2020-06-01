import React, { Component } from 'react';
import { Route } from 'react-router';
import  Layout from './components/Layout';
import Signup from './Pages/Signup';
import Login from './Pages/Login';
import NewJoke from './Pages/NewJoke';
import AllJokes from './Pages/AllJokes';
import { AuthContextComponent } from './AuthContext';
import PrivateRoute from './PrivateRoute';
import Logout from './Pages/Logout';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <AuthContextComponent>
                <Layout>
                    <Route exact path='/signup' component={Signup} />
                    <Route exact path='/login' component={Login} />
                    <Route exact path='/logout' component={Logout} />
                    <PrivateRoute exact path='/newjoke' component={NewJoke} />
                    <PrivateRoute exact path='/alljokes' component={AllJokes} />
                </Layout>
            </AuthContextComponent>
        );
    }
}

