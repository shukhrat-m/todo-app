import { useState } from 'react';
import { useParams } from 'react-router';
import { NavLink, useNavigate } from 'react-router-dom';
import { Button, Message, Segment } from 'semantic-ui-react';
import apiService from '../../services/apiService';

function TaskDelete() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [errorMessage, setErrorMessage] = useState<string>('') 

    const onDeleteTask = () => {
        apiService.Tasks.delete(id!)
            .then(() => {
                navigate("/");
            })
            .catch((err) => {
                setErrorMessage(err.response.data);
            });
    }

    return (
        <div>
            {errorMessage && <Message negative>
                <Message.Header>{errorMessage}</Message.Header>
            </Message>}

            <Segment>
                Do you really want to delete this task?
                <div>
                    <Button.Group>
                        <Button as={NavLink} to={`/`}>No</Button>
                        <Button.Or />
                        <Button onClick={onDeleteTask} color="red">Yes</Button>
                    </Button.Group>
                </div>
            </Segment>
        </div>
    )
}

export default TaskDelete;