import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Container } from 'semantic-ui-react';
import NavBar from './components/NavBar/NavBar';
import TaskCreateOrEdit from './components/Task/TaskCreateOrEdit';
import TaskDelete from './components/Task/TaskDelete';
import TaskList from './components/TaskList/TaskList';

const App = () => {
  return (
      <div className="App">
          <BrowserRouter>
              <Container>
                  <NavBar />
                  <Routes>
                      <Route path="/" index element={<TaskList />} />
                      <Route path="/create" element={<TaskCreateOrEdit isUpdate={false} />} />
                      <Route path="/update/:id" element={<TaskCreateOrEdit isUpdate={true} />} />
                      <Route path="/delete/:id" element={<TaskDelete />} />
                  </Routes>
              </Container>
          </BrowserRouter>
    </div>
  );
}

export default App;
