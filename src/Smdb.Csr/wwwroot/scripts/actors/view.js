import { $, apiFetch, renderStatus, getQueryParam } from '/scripts/common.js';

(async function initActorView() {
  const id = getQueryParam('id');
  const statusEl = $('#status');
  const moviesStatusEl = $('#movies-status');
  const moviesListEl = $('#actor-movies');

  if (!id) {
    renderStatus(statusEl, 'err', 'Missing ?id in URL.');
    return;
  }

  try {
    const a = await apiFetch(`/actors/${encodeURIComponent(id)}`);
    $('#actor-id').textContent = a.id;
    $('#actor-name').textContent = a.name;
    $('#actor-birthYear').textContent = a.birthYear;
    $('#actor-desc').textContent = a.description || '—';
    $('#edit-link').href = `/actors/edit.html?id=${encodeURIComponent(a.id)}`;
    renderStatus(statusEl, 'ok', 'Actor loaded successfully.');
  } catch (err) {
    renderStatus(statusEl, 'err', `Failed to load actor ${id}: ${err.message}`);
    return;
  }

  try {
    const page = 1;
    const size = 10;
    const payload = await apiFetch(`/actors/${encodeURIComponent(id)}/movies?page=${page}&size=${size}`);
    const items = Array.isArray(payload) ? payload : (payload.data || []);

    moviesListEl.replaceChildren();

    if (items.length === 0) {
      renderStatus(moviesStatusEl, 'warn', 'No movies found for this actor.');
    } else {
      renderStatus(moviesStatusEl, '', '');
      for (const m of items) {
        const li = document.createElement('li');
        li.textContent = `${m.title} (${m.year})`;
        moviesListEl.appendChild(li);
      }
    }
  } catch (err) {
    renderStatus(moviesStatusEl, 'err', `Failed to load movies: ${err.message}`);
  }
})();
