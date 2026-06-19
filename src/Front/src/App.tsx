import { Outlet } from "react-router-dom";

function App() {
  return (
    <>
      {/*<Sidebar />*/}
      <div className="ml-64 p-4">
        <Outlet />
      </div>
    </>
  );
}

export default App;
