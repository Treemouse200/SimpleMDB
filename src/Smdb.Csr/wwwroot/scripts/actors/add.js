import { $, apiFetch, renderStatus } from '/scripts/common.js';

function captureActorForm(form) {
  const name = form.name.value.trim();
  const birthYear = Number(form.birthYear.value);
  const description = form.description.value.trim();
  return { name, birthYear, description };
}

(async function initActorAdd() {
  const form = $('#actor-form');
  const statusEl = $('#status');
  renderStatus(statusEl, 'ok', 'New actor. You can edit and save.');

  form.addEventListener('submit', async (ev) => {
    ev.preventDefault();
    const payload = captureActorForm(form);

    if (payload.name.value > 50)
    {
      renderStatus(statusEl, 'err', 'Name should not be greater than 100.')
      return
    }

    try {
      const created = await apiFetch('/actors', {
        method: 'POST',
        body: JSON.stringify(payload),
      });
      renderStatus(
        statusEl,
        'ok',
        `Created actor #${created.id} "${created.name}" (${created.birthYear}).`
      );
      form.reset();
    } catch (err) {
      renderStatus(statusEl, 'err', `Create failed: ${err.message}`);
    }
  });
})();
