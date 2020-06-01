import React from 'react';
import axios from 'axios';

class NewJoke extends React.Component {

    state = {
        joke: {},
        loading: true
    }
    componentDidMount = async () => {
        await this.onNewJoke();
    }
    onNewJoke = async () => {
        const { data } = await axios.get('api/joke/getjoke');
        this.setState({ joke: data, loading: false });
    }
    onLikeClick = async () => {
        const liked = true;
        await axios.post('api/joke/likejoke', { joke: this.state.joke, liked })
    }
    onDislikeClick = async () => {
        const liked = false;
        await axios.post('api/joke/likejoke', { joke: this.state.joke, liked })
    }
    generateLikeButtons = () => {
        const { dateLiked } = this.state.joke.userLikedJokes[0];
        var datePosted = new Date(dateLiked);
        var dif = new Date() - datePosted.getTime();
        var difMin = Math.floor((dif / 1000) / 60 / 1000); 
        if (difMin < 10) {
            const { liked } = this.state.joke.userLikedJokes;
            if (liked) {
                return <React.Fragment>
                    <button className='btn btn-primary' disabled>Like</button>
                    <button className='btn btn-primary' onClick={this.onDislikeClick}>Dislike</button>
                </React.Fragment>
            }
           
            return <React.Fragment>
                <button className='btn btn-primary' onClick={this.onLikeClick}>Like</button>
                <button className='btn btn-primary' disabled>Dislike</button>
            </React.Fragment>
        }
        return <React.Fragment>
            <button className='btn btn-primary' disabled>Like</button>
            <button className='btn btn-primary' disabled>Dislike</button>
        </React.Fragment>
    }



    render() {
        const { joke, loading } = this.state;
        return (
            <div>
                <div className='well'>
                    {loading && <h4>Loading...</h4>}
                    {!loading &&
                        <div>
                            <div>
                                <h3>Question: {joke.setup}</h3>
                                <br />
                                <h3>Answer: {joke.punchline}</h3>
                                <br />
                            </div>
                        {this.state.joke.userLikedJokes === null &&
                            <React.Fragment>
                                <button className='btn btn-primary' onClick={this.onLikeClick}>Like</button>
                                <button className='btn btn-primary' onClick={this.onDislikeClick}>Dislike</button>
                            </React.Fragment>}
                        {this.state.joke.userLikedJokes !== null && this.generateLikeButtons()}
                        </div>
                    }
                </div>
                <div>
                    <button className='btn btn-success' onClick={this.onNewJoke}>Another joke please...</button>
                </div>
            </div>
        );
    }
}

export default NewJoke;