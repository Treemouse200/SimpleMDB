import { $, apiFetch, renderStatus } from '/scripts/common.js';

function captureUserForm(form) {
  const userName = form.username.value.trim();
  const email = form.email.value.trim();
  return { userName, email };
}

(async function initUserAdd() {
  const form = $('#user-form');
  const statusEl = $('#status');
  renderStatus(statusEl, 'ok', 'New user. You can edit and save.');

  form.addEventListener('submit', async (ev) => {
    ev.preventDefault();
    const payload = captureUserForm(form);

    try {
      const created = await apiFetch('/users', {
        method: 'POST',
        body: JSON.stringify(payload),
      });
      renderStatus(
        statusEl,
        'ok',
        `Created user #${created.id} "${created.userName}" (${created.email}).`
      );
      form.reset();
    } catch (err) {
      renderStatus(statusEl, 'err', `Create failed: ${err.message}`);
    }
  });
})();
