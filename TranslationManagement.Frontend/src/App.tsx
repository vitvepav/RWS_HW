import { Link, Route, Routes } from 'react-router-dom'
import './App.css'
import TranslationJobList from './pages/TranslationJobList/TranslationJobList'
import HomePage from './pages/HomePage/HomePage'
import TranslatorList from './pages/TranslatorList/TranslatorList'

function App() {
  return (
    <>
    <nav>
      <ul>
      <li><Link to="/">Home</Link></li>
        <li><Link to="/translationJob">TranslationJobs</Link></li>
        <li><Link to="/translator">Translators</Link></li>
      </ul>
    </nav>
      <Routes>
        <Route path="/" element={<HomePage />}/>
        <Route path="/translationJob" element={<TranslationJobList />}/>
        <Route path="/translator" element={<TranslatorList />}/>
      </Routes>
    </>
  )
}

export default App
