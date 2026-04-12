import { $, apiFetch, renderStatus, getQueryParam } from '/scripts/common.js';

function captureUserForm(form) {
  const userName = form.username.value.trim();
  const email = form.email.value.trim();
  return { userName, email };
}

(async function initUserEdit() {
  const id = getQueryParam('id');
  const form = $('#user-form');
  const statusEl = $('#status');

  if (!id) {
    renderStatus(statusEl, 'err', 'Missing ?id in URL.');
    form.querySelectorAll('input,textarea,button,select').forEach(el => el.disabled = true);
    return;
  }

  try {
    const u = await apiFetch(`/users/${encodeURIComponent(id)}`);
    form.username.value = u.userName ?? '';
    form.email.value = u.email ?? '';
    renderStatus(statusEl, 'ok', 'Loaded user. You can edit and save.');
  } catch (err) {
    renderStatus(statusEl, 'err', `Failed to load data: ${err.message}`);
    return;
  }

  form.addEventListener('submit', async (ev) => {
    ev.preventDefault();
    const payload = captureUserForm(form);

    try {
      const updated = await apiFetch(`/users/${encodeURIComponent(id)}`, {
        method: 'PUT',
        body: JSON.stringify(payload),
      });
      renderStatus(
        statusEl,
        'ok',
        `Updated user #${updated.id} "${updated.userName}" (${updated.email}).`
      );
    } catch (err) {
      renderStatus(statusEl, 'err', `Update failed: ${err.message}`);
    }
  });
})();
