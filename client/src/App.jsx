import { useState } from 'react'
import dude from "./images/Chill_guy.jpg"
import './css/App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div className="chill-guy">
        <img src={dude} width="301" height="331"></img>
      </div>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          React project for Language Gamification
        </p>
        <p>
          Dont forget, we are all just a chill guy trying to code.
        </p>
      </div>
    </>
  )
}

export default App
