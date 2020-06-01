import React from 'react';
import axios from 'axios';

class NewJoke extends React.Component {

    state = {
            jokes: []
    }
    componentDidMount = async () => {
        const { data } = await axios.get('api/joke/getalljokes');
        this.setState({ jokes: data });
    }

    render()
    {
        return(
            <div>                
                {this.state.jokes.map(j =>
                    <div className='well'>
                        <h3>Question: {j.setup}</h3>
                        <br />
                        <h3>Answer: {j.punchline}</h3>
                        <br />
                        <h5>Likes: {j.userLikedJokes.filter(ulj => ulj.liked === true).length}</h5>
                        <h5>Dislikes: {j.userLikedJokes.filter(ulj => ulj.liked === false).length}</h5>
                    </div>)}
            </div>
        );

    }

}

export default NewJoke;