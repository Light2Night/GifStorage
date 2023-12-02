import { Home } from "./components/Home";
import { FetchData } from "./components/FetchData";
import { GifList } from "./components/GifList";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/gif-list',
    element: <GifList />
  }
];

export default AppRoutes;
