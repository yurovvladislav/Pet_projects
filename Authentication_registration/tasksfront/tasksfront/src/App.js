import './App.css';
import React from 'react';
import { useState, useEffect } from 'react';
import axios from 'axios';
import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
// import Header from "./components/Header";
// import Registration from "./components/Registration";
// import Authorization from "./components/Authorization";
// import Tasks from "./components/Tasks";


axios.defaults.xsrfCookieName = 'csrftoken';
axios.defaults.xsrfHeaderName = 'X-CSRFToken';
axios.defaults.withCredentials = false;

const client = axios.create({
  // baseURL: "https://summerpractise-vlad21.amvera.io/"
     baseURL: "http://127.0.0.1:8000"
});


class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      currentUser: undefined,
      registrationToggle: false,
      email: '',
      username: '',
      password: '',
      inscription: '',
      tasks: [],
    };
    this.update_form_btn = this.update_form_btn.bind(this);
    this.submitRegistration = this.submitRegistration.bind(this);
    this.submitLogin = this.submitLogin.bind(this);
    this.submitLogout = this.submitLogout.bind(this);
  }

  componentDidMount() {
    client.get("/tasks/user")
      .then((res) => {
        this.setState({ currentUser: true });
      })
      .catch((error) => {
        this.setState({ currentUser: false });
      });
  }

  update_form_btn() {
    const { registrationToggle } = this.state;
    if (registrationToggle) {
      document.getElementById("form_btn").innerHTML = "Register";
      this.setState({ registrationToggle: false });
    } else {
      document.getElementById("form_btn").innerHTML = "Log in";
      this.setState({ registrationToggle: true });
    }
  }

  submitRegistration(e) {
    e.preventDefault();
    const { email, username, password } = this.state;
    client.post("/tasks/register", { email, username, password })
      .then((res) => {
        client.post("/tasks/login", { email, password })
          .then((res) => {
            this.setState({ currentUser: true });
            this.setState({inscription: ''});
          });
      })
      .catch((error) => {
        this.setState({ currentUser: false });
        this.setState({ inscription: 'Такой пользователь уже существует'});
      })
  }

  submitLogin(e) {
    e.preventDefault();
    const { email, password } = this.state;
    client.post("/tasks/login", { email, password })
      .then((res) => {
        this.setState({ currentUser: true });
        this.setState({ inscription: ''});
        this.setState({ tasks: res.data})
      })
      .catch((error) => {
        this.setState({ currentUser: false });
        this.setState({inscription: 'Неверно введён логин или пароль'});
      });
      
  }

  submitLogout(e) {
    e.preventDefault();
    client.post("/tasks/logout", { withCredentials: true })
      .then((res) => {
        this.setState({ currentUser: false });
      });
  }

  render() {
    const { currentUser, registrationToggle, email, username, password } = this.state;
    if (currentUser) {
      return (
        <div>
          <Navbar bg="dark" variant="dark">
            <Container>
              <Navbar.Brand>Tasks</Navbar.Brand>
              <Navbar.Toggle />
              <Navbar.Collapse className="justify-content-end">
                <Navbar.Text>
                  <form onSubmit={this.submitLogout}>
                    <Button type="submit" variant="light">Выйти</Button>
                  </form>
                </Navbar.Text>
              </Navbar.Collapse>
            </Container>
          </Navbar>
          <div className="center">
            <h2>Добро пожаловать</h2>
          </div>
        </div>
      );
    }
    return (
      <div>
        <Navbar bg="dark" variant="dark">
          <Container>
            <Navbar.Brand>Tasks</Navbar.Brand>
            <Navbar.Toggle />
            <Navbar.Collapse className="justify-content-end">
              <Navbar.Text>
                <Button id="form_btn" onClick={this.update_form_btn} variant="light">Регистрация</Button>
              </Navbar.Text>
            </Navbar.Collapse>
          </Container>
        </Navbar>
        {
          registrationToggle ? (
            <div className="center">
              <Form onSubmit={this.submitRegistration}>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                <Form.Label>Почта</Form.Label>
                <Form.Control type="email" placeholder="Enter email" value={email} onChange={(e) => this.setState({email: e.target.value})} />
                {/* <Form.Text className="text-muted">
                  We'll never share your email with anyone else.
                </Form.Text> */}
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicUsername">
                <Form.Label>Имя</Form.Label>
                <Form.Control type="text" placeholder="Enter username" value={username} onChange={(e) => this.setState({username: e.target.value})} />
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Пароль</Form.Label>
                <Form.Control type="password" placeholder="Password" value={password} onChange={(e) => this.setState({password: e.target.value})} />
                </Form.Group>
                <Button variant="primary" type="submit">
                Зарегистрироваться
                </Button>
                
              </Form>
            </div>
          ) : (
            <div className="center">
              <Form onSubmit={this.submitLogin}>
              <Form.Group className="mb-3" controlId="formBasicEmail">
              <Form.Label>Почта</Form.Label>
              <Form.Control type="email" placeholder="Enter email" value={email} onChange={(e) => this.setState({email: e.target.value})} />
              <Form.Text className="text-muted">
              </Form.Text>
              </Form.Group>
              <Form.Group className="mb-3" controlId="formBasicPassword">
              <Form.Label>Пароль</Form.Label>
              <Form.Control type="password" placeholder="Password" value={password} onChange={(e) => this.setState({password: e.target.value})} />
              </Form.Group>
              <Button variant="primary" type="submit">
              Войти
              </Button>
              <h3>{this.state.inscription}</h3>
              </Form>
            </div>
          )
        }
      </div>
    );
  }
}

export default App;